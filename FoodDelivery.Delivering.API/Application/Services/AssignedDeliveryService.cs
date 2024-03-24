using FoodDelivery.Delivering.API.Application.Commands.CourierCommands;
using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public class AssignedDeliveryService : BackgroundService
    {
        public AssignedDeliveryService(IServiceProvider serviceProvider,
            IMediator mediator,
            IAssignDeliveryQueue queue,
            IDeliverySignalRHubService deliverySignalRHubService)
        {
            _mediator = mediator;
            _serviceProvider = serviceProvider;
            _queue = queue;
            _deliverySignalRHubService = deliverySignalRHubService;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly IAssignDeliveryQueue _queue;
        private readonly IDeliverySignalRHubService _deliverySignalRHubService;
        private readonly IMediator _mediator;
        private int _timeout = TimeSpan.FromSeconds(60).Milliseconds;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
           
            while (!stoppingToken.IsCancellationRequested)
            {
                var  deliveryContext = await _queue.DequeueAsync(stoppingToken); 
              
                if (deliveryContext.CourierId is null)
                {
                    var couriers = await GetCouriersForDelivery(deliveryContext.Delivery);
                    if (!couriers.Any())
                    {
                        RetryAssignDeliveryToCourier(deliveryContext.Delivery);
                        continue;
                    }
                    deliveryContext.CourierId = couriers.First().Id;
                    
                }

                await AssignDeliveryToCourier(deliveryContext.Delivery.Id, deliveryContext.CourierId.Value);
                
                var timer = new Timer(async state =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<DeliveryContext>();
                    var context = state as AssignDeliveryContext;

                    var assignDelivery = await db.AssignDeliveries.Where(x => x.DeliveryId == context.Delivery.Id && x.CourierId == context.CourierId.Value).FirstOrDefaultAsync();
                    var delivery = await db.Deliveries.Where(x=> x.Id == context.Delivery.Id).FirstOrDefaultAsync();
                    if (delivery is null)
                        return;

                    if (assignDelivery is null)
                        return;
                    //Delivery was not taken of courier
                    if(delivery.DeliveryStatus == DeliveryStatus.Created)
                    {
                        var setMissCommand = new SetMissAssignDeliveryStatusCommand(context.Delivery.Id , context.CourierId.Value);
                        await _mediator.Send(setMissCommand);
                        var newCouriers = await GetCouriersForDelivery(delivery);
                        await _queue.EnqueueAsync(new AssignDeliveryContext(delivery, newCouriers.FirstOrDefault()?.Id));
                    }

                    if (false && assignDelivery.Status == AssignDeliveryStatus.WaitingConfirm)
                    {
                        assignDelivery.SetMissStatus();
                        db.AssignDeliveries.Update(assignDelivery);
                        db.SaveChanges();

                        var newCouriers = await GetCouriersForDelivery(assignDelivery.Delivery);
                        await _queue.EnqueueAsync(new AssignDeliveryContext(assignDelivery.Delivery, newCouriers.FirstOrDefault()?.Id));
                    }


                },  deliveryContext, _timeout, Timeout.Infinite);

            }
        }
        
        public void RetryAssignDeliveryToCourier(Delivery Delivery)
        {
            var retryTimer = new Timer(async context =>
            {
                var retryDelivery = (Delivery)context;
                await _queue.EnqueueAsync(new(retryDelivery, null));

            }, Delivery, TimeSpan.FromSeconds(15).Milliseconds, Timeout.Infinite);
        }

        public async Task AssignDeliveryToCourier(long DeliveryId, long CourierId)
        {
            var createAssignDeliveryCommand = new CreateAssignDeliveryCommand(DeliveryId, CourierId);
            await _mediator.Send(createAssignDeliveryCommand);
        }

        public async Task<List<Courier>> GetCouriersForDelivery(Delivery delivery)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DeliveryContext>();

            return await db.Couriers
                .Where(x => x.WorkStatus == WorkStatus.AtWork)
                .Where(x => x.WorkAddress.Country == delivery.RecipientAddress.Country)
                .Where(x => x.WorkAddress.City == delivery.RecipientAddress.City)
                .Where(e =>
                    !db.AssignDeliveries
                    .Where(x => x.DeliveryId == x.DeliveryId && x.Status == AssignDeliveryStatus.Miss
                    || x.Status == AssignDeliveryStatus.WaitingConfirm)
                    .Any(x => x.CourierId == e.Id)
                )
                .OrderBy(e =>
                    db.Deliveries
                    .Where(x => x.CourierId == e.Id)
                    .Where(x => x.DeliveryStatus == DeliveryStatus.Delivered)
                    .Count())
                .ToListAsync();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
