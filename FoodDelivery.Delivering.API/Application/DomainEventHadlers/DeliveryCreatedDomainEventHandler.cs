using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using FoodDelivery.Delivering.Extention;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class DeliveryCreatedDomainEventHandler
         : INotificationHandler<DeliveryCreatedDomainEvent>
    {
        private readonly ILogger _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryCreatedDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeliveryCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.Delivery.Id, @event.Delivery.DeliveryStatus);

            var integrationEvent = new DeliveryCreatedIntegrationEvent(@event.Delivery.Id);
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
