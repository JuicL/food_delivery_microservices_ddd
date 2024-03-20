using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler
         : INotificationHandler<AssignDeliveryStatusChangedToWaitingConfirmDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;

        public AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AssignDeliveryStatusChangedToWaitingConfirmDomainEvent @event, CancellationToken cancellationToken)
        {
            //DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.Delivery.Id, @event.Delivery.DeliveryStatus);
            var delivery = await _deliveryRepository.GetByIdAsync(@event.assignDelivery.DeliveryId);
            if (delivery is null)
                throw new Exception("Delivery not found");
            
            // TODO : SignalR Hub message 

            var integrationEvent = new AppointCourierForDeliveryIntegrationEvent(@event.assignDelivery.CourierId, delivery.Id, 
                delivery.SenderAddress.GetFullAddress(),delivery.RecipientAddress.GetFullAddress());
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
