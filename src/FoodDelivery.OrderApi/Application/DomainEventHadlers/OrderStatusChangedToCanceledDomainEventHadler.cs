using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.DTOs;
using FoodDelivery.OrderApi.Extention;
using FoodDelivery.OrderApi.Infrastructure.Repository.Implementation;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers;

public class OrderStatusChangedToCanceledDomainEventHadler
     : INotificationHandler<OrderStatusChangedToCanceledDomainEvent>
{
    private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderStatusChangedToCanceledDomainEventHadler> _logger;

    public OrderStatusChangedToCanceledDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
        IOrderRequestRepository orderRequestRepository, 
        IMediator mediator,
        ILogger<OrderStatusChangedToCanceledDomainEventHadler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _orderRequestRepository = orderRequestRepository ?? throw new ArgumentNullException(nameof(orderRequestRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToCanceledDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.OrderId, OrderStatus.Canceled);

        var order = await _orderRequestRepository.GetAsync(domainEvent.OrderId);
        var orderItems = order.Dishes.Select(x => new OrderItem(x.Id, x.Units)).ToList();

        var integrationEvent = new OrderStatusChangedToCanceledIntegrationEvent(order.Id);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}
