using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Domain.Events;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.DomainEventHandlers
{
    public class AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler
         : INotificationHandler<AssignDeliveryStatusChangedToWaitingConfirmDomainEvent>
    {
        private readonly ILogger<AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler> _logger;
        private readonly IDeliveryIntegrationEventService _deliveryIntegrationEventService;
        private readonly IMediator _mediator;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly IDeliverySignalRHubService _deliverySignalRHubService;
        public AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler(IDeliveryIntegrationEventService dileveryIntegrationEventService,
            IMediator mediator, IDeliveryRepository deliveryRepository, 
            ILogger<AssignDeliveryStatusChangedToWaitingConfirmDomainEventHandler> logger, IDeliverySignalRHubService deliverySignalRHubService, ICourierRepository courierRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _deliveryIntegrationEventService = dileveryIntegrationEventService ?? throw new ArgumentNullException(nameof(dileveryIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _deliverySignalRHubService = deliverySignalRHubService ?? throw new ArgumentNullException(nameof(deliverySignalRHubService));
            _courierRepository = courierRepository;
        }

        public async Task Handle(AssignDeliveryStatusChangedToWaitingConfirmDomainEvent @event, CancellationToken cancellationToken)
        {
            //DeliveryApiTrace.LogDeliveryStatusUpdated(_logger, @event.Delivery.Id, @event.Delivery.DeliveryStatus);
            var delivery = await _deliveryRepository.GetByIdAsync(@event.assignDelivery.DeliveryId);
            if (delivery is null)
                throw new Exception("Delivery not found");
            var courier = await _courierRepository.GetByIdAsync(@event.assignDelivery.CourierId);
            if (courier is null)
                throw new Exception("Courier not found");
            
            //SignalR
            await _deliverySignalRHubService.AssignDeliveryRequestAsync(courier.PhoneNumber.Number, delivery.Id.ToString(),
                delivery.SenderAddress.GetFullAddress(), delivery.RecipientAddress.GetFullAddress());
            
            //IntegrationEvent 
            var integrationEvent = new AppointCourierForDeliveryIntegrationEvent(@event.assignDelivery.CourierId, delivery.Id, 
                delivery.SenderAddress.GetFullAddress(),delivery.RecipientAddress.GetFullAddress());
            await _deliveryIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

        }
    }
}
