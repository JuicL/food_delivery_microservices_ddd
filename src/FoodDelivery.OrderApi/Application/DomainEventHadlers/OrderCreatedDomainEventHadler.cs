using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.ValueObjects;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.Extention;
using MediatR;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers
{
    public class OrderCreatedDomainEventHadler
         : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public OrderCreatedDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
            IMediator mediator, IUserRepository userRepository, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }

        public async Task Handle(OrderCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            OrderingApiTrace.LogOrderStatusUpdated(_logger, @event.order.Id, @event.order.OrderStatus);
            var user = await _userRepository.GetAsync(@event.userId);
            if(user is null)
            {
                user = new User(@event.userId, @event.userName,@event.Phone);
                await _userRepository.CreateAsync(user);
            }
            await _userRepository.UnitOfWork.SaveEntitiesAsync();

            var integrationEvent = new OrderStatusChangedToCreatedIntegrationEvent(@event.order.Id, @event.userId, @event.userName);
            await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
            
        }
    }
}
