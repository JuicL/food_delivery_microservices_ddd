using FoodDelivery.Delivering.API.Application.Commands.CourierCommands;
using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using FoodDelivery.Delivering.Extention;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class DeliveryStatusChangedToDeliveredDomainEventHandler
            : INotificationHandler<DeliveryStatusChangedToDeliveredDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryStatusChangedToDeliveredDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
                                 
        public async Task Handle(DeliveryStatusChangedToDeliveredDomainEvent @event, CancellationToken cancellationToken)
        {
            DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.Delivery.Id,DeliveryStatus.Delivered);
            var deliveredAssignDeliveryCommand = new SetDeliveredAssignDeliveryStatusCommand(@event.Delivery.Id, @event.Delivery.CourierId.Value);
            await _mediator.Send(deliveredAssignDeliveryCommand);
            
            var setOnDeliveryCourierStatus = new SetWorkOffCourierStatusCommand(@event.Delivery.CourierId.Value);
            await _mediator.Send(setOnDeliveryCourierStatus); 

            var integrationEvent = new DeliveryStatusChangedToDeliveredIntegrationEvent(@event.Delivery.Id,@event.Delivery.OrderId);
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
        }
    }
}
