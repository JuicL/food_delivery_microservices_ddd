using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.Extention;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers;
public class OrderStatusChangedToAwaitingValidationDomainEventHadler
     : INotificationHandler<OrderStatusChangedToAwaitingValidationDomainEvent>
{
    private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderStatusChangedToAwaitingValidationDomainEventHadler> _logger;

    public OrderStatusChangedToAwaitingValidationDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
        IOrderRequestRepository orderRequestRepository,
        IMediator mediator,
        ILogger<OrderStatusChangedToAwaitingValidationDomainEventHadler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _orderRequestRepository = orderRequestRepository ?? throw new ArgumentNullException(nameof(orderRequestRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToAwaitingValidationDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.OrderId, OrderStatus.AwaitingValidation);

        var order = await _orderRequestRepository.GetAsync((int)domainEvent.OrderId);
        var orderItems = order.Dishes.Select(x => x.DishId).ToList();

        var integrationEvent = new OrderStatusChangedToAwaitingValidationIntegrationEvent(order.Id, order.BranchId, orderItems);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

    }
}
