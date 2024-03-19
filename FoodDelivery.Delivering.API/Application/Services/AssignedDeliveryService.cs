using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public class AssignedDeliveryService : BackgroundService, IAssignedDeliveryService
    {
        public AssignedDeliveryService(DeliveryContext deliveryContext,
            IAssignDeliveryRepository assignDeliveryRepositor,
            AssignDeliveryQueue queue)
        {
            _deliveryContext = deliveryContext;
            _assignDeliveryRepositor = assignDeliveryRepositor;
            _queue = queue;
        }

        private readonly DeliveryContext _deliveryContext;
        private readonly IAssignDeliveryRepository _assignDeliveryRepositor;
        private readonly AssignDeliveryQueue _queue;
        private int _timeout = TimeSpan.FromSeconds(60).Milliseconds;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!_queue.Any())
                    continue;
                var deliveryContext = _queue.Dequeue();

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
                    var context = state as TimerCallBackContext;

                    var assignDelivery = await _assignDeliveryRepositor
                                        .GetByCourierAndDeliveryIdsAsync(context.AssignDeliveryContext.Delivery.Id, context.AssignDeliveryContext.CourierId.Value);

                    if (assignDelivery is null)
                        return;

                    if (assignDelivery.Status == AssignDeliveryStatus.WaitingConfirm)
                    {
                        assignDelivery.SetMissStatus();
                        await _assignDeliveryRepositor.UpdateAsync(assignDelivery);
                        await _assignDeliveryRepositor.UnitOfWork.SaveEntitiesAsync();

                        var newCouriers = await GetCouriersForDelivery(assignDelivery.Delivery);
                        _queue.Enqueue(new AssignDeliveryContext(assignDelivery.Delivery, newCouriers.FirstOrDefault()?.Id));
                    }


                }, new TimerCallBackContext(_deliveryContext, deliveryContext), _timeout, Timeout.Infinite);

            }
        }
        
        public void RetryAssignDeliveryToCourier(Delivery Delivery)
        {
            var retryTimer = new Timer(context =>
            {
                var retryDelivery = (Delivery)context;
                _queue.Enqueue(new(retryDelivery, null));

            }, Delivery, TimeSpan.FromSeconds(15).Milliseconds, Timeout.Infinite);
        }

        public async Task AssignDeliveryToCourier(long DeliveryId, long CourierId)
        {
            var assignDelivery = new AssignDelivery(DeliveryId, CourierId);
            await _assignDeliveryRepositor.CreateAsync(assignDelivery);
            await _assignDeliveryRepositor.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<List<Courier>> GetCouriersForDelivery(Delivery delivery)
        {
            return await _deliveryContext.Couriers
                .Where(x => x.WorkStatus == WorkStatus.AtWork)
                .Where(x => x.WorkAddress.Country == delivery.RecipientAddress.Country)
                .Where(x => x.WorkAddress.City == delivery.RecipientAddress.City)
                .Where(e =>
                    !_deliveryContext.AssignDeliveries
                    .Where(x => x.DeliveryId == x.DeliveryId && x.Status == AssignDeliveryStatus.Miss
                    || x.Status == AssignDeliveryStatus.WaitingConfirm)
                    .Any(x => x.CourierId == e.Id)
                )
                .OrderBy(e =>
                    _deliveryContext.Deliveries
                    .Where(x => x.CourierId == e.Id)
                    .Where(x => x.DeliveryStatus == DeliveryStatus.Delivered)
                    .Count())
                .ToListAsync();
        }

       
    }
}
