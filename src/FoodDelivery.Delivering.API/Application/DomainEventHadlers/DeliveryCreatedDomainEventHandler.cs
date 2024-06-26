﻿using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.API.Application.Services;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.Events;
using FoodDelivery.Delivering.Extention;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class DeliveryCreatedDomainEventHandler
         : INotificationHandler<DeliveryCreatedDomainEvent>
    {
        private readonly ILogger<DeliveryCreatedDomainEventHandler> _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IAssignDeliveryQueue _assignDeliveryQueue;
        public DeliveryCreatedDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
             IAssignDeliveryQueue assignDeliveryQueue,
            IMediator mediator, IDeliveryRepository deliveryRepository, ILogger<DeliveryCreatedDomainEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _assignDeliveryQueue = assignDeliveryQueue ?? throw new ArgumentNullException(nameof(assignDeliveryQueue));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeliveryCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.Delivery.Id, @event.Delivery.DeliveryStatus);

            await _assignDeliveryQueue.EnqueueAsync(new(@event.Delivery));

            var orderTakenToDeliveryEvent = new OrderTakenToDeliveryIntegrationEvent(@event.Delivery.OrderId,@event.Delivery.Id);
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(orderTakenToDeliveryEvent);

            //var integrationEvent = new DeliveryCreatedIntegrationEvent(@event.Delivery.OrderId);
            //await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
