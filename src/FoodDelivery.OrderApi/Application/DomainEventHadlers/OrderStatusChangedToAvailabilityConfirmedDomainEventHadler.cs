using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.Extention;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers;

public class OrderStatusChangedToAvailabilityConfirmedDomainEventHadler
     : INotificationHandler<OrderStatusChangedToAvailabilityConfirmedDomainEvent>
{
    private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderStatusChangedToAvailabilityConfirmedDomainEventHadler> _logger;

    public OrderStatusChangedToAvailabilityConfirmedDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
        IOrderRequestRepository orderRequestRepository,
        IMediator mediator,
        ILogger<OrderStatusChangedToAvailabilityConfirmedDomainEventHadler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _orderRequestRepository = orderRequestRepository ?? throw new ArgumentNullException(nameof(orderRequestRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToAvailabilityConfirmedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.OrdeId, OrderStatus.AvailabilityConfirmed);

        var order = await _orderRequestRepository.GetAsync((int)domainEvent.OrdeId);
        var totalPrice = order.Dishes.Sum(x => x.Price.Amount * x.Units);
        
        if(order.PaymentMethod == PaymentMethod.Cash)
        {
            var setInProcessCommand = new SetPaidOrderStatusCommand(order.Id);
            await _mediator.Send(setInProcessCommand);
            return;
        }

        var integrationEvent = new OrderStatusChangedToAvailabilityConfirmedIntegrationEvent(order.Id, totalPrice);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }

}
