using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.DTOs;
using FoodDelivery.OrderApi.Extention;
using MediatR;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers;

public class OrderStatusChangedToDeliveredDomainEventHadler
     : INotificationHandler<OrderStatusChangedToDeliveredDomainEvent>
{
    private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public OrderStatusChangedToDeliveredDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
        IOrderRequestRepository orderRequestRepository,
        IMediator mediator,
        ILogger logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _orderRequestRepository = orderRequestRepository ?? throw new ArgumentNullException(nameof(orderRequestRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToDeliveredDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.OrderId, OrderStatus.Delivered);

        var order = await _orderRequestRepository.GetAsync(domainEvent.OrderId);
        var orderItems = order.Dishes.Select(x => new OrderItem(x.Id, x.Units)).ToList();

        var integrationEvent = new OrderStatusChangedToDeliveredIntegrationEvent(order.Id);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}