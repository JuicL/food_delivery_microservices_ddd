using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.OrderApi.DTOs;
using FoodDelivery.OrderApi.Extention;
using MediatR;

namespace FoodDelivery.OrderApi.Application.DomainEventHadlers;

public class OrderStatusChangedToPaidDomainEventHadler
     : INotificationHandler<OrderStatusChangedToPaidDomainEvent>
{
    private readonly IOrderIntegrationEventService _orderingIntegrationEventService;
    private readonly IOrderRequestRepository _orderRequestRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderStatusChangedToPaidDomainEventHadler> _logger;

    public OrderStatusChangedToPaidDomainEventHadler(IOrderIntegrationEventService orderingIntegrationEventService,
        IOrderRequestRepository orderRequestRepository,
        IMediator mediator,
        ILogger<OrderStatusChangedToPaidDomainEventHadler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
        _orderRequestRepository = orderRequestRepository ?? throw new ArgumentNullException(nameof(orderRequestRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToPaidDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        OrderingApiTrace.LogOrderStatusUpdated(_logger, domainEvent.OrderId, OrderStatus.Paid);

        var order = await _orderRequestRepository.GetAsync(domainEvent.OrderId);

        var orderResponse = new OrderResponseDTO(order.Id, order.UserId, order.UserName, order.Phone.Number, order.DeliveryAddress.GetFullAddress(),
               order.BranchId, order.RestaurantName, order.RestaurantAddress.GetFullAddress(), order.PaymentMethod.Name, order.OrderTime,
               order.Dishes.Select(x => new DishesDTO() { Id = x.DishId, Name = x.Name, Price = x.Price.Amount, Weight = x.Weight.Grams,Units = x.Units }).ToList(),
               order.Description
               );

        var integrationEvent = new OrderStatusChangedToPaidIntegrationEvent(orderResponse);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
    
}
