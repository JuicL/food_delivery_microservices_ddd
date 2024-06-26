﻿using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using FoodDelivery.Delivering.Extention;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEventEventHandler
            : INotificationHandler<DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEvent>
    {
        private readonly ILogger<DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEventEventHandler> _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEventEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, 
            ILogger<DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEventEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
                                 
        public async Task Handle(DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEvent @event, CancellationToken cancellationToken)
        {
            DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.DeliveryId,DeliveryStatus.ArrivedAtDeliveryLocation);

            var integrationEvent = new DeliveryStatusChangedToArrivedAtDeliveryLocationIntegrationEvent(@event.DeliveryId);
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
