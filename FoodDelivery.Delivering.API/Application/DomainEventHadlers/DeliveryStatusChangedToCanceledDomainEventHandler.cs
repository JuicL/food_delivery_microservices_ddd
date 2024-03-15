using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using FoodDelivery.Delivering.Extention;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class DeliveryStatusChangedToCanceledDomainEventHandler
            : INotificationHandler<DeliveryStatusChangedToCanceledDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryStatusChangedToCanceledDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
                                 
        public async Task Handle(DeliveryStatusChangedToCanceledDomainEvent @event, CancellationToken cancellationToken)
        {
            DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.DeliveryId,DeliveryStatus.Canceled);

            var integrationEvent = new DeliveryStatusChangedToCanceledIntegrationEvent(@event.DeliveryId);
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
