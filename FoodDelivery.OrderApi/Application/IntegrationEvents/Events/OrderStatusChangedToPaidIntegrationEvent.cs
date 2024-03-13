using FoodDelibery.EventBus.Events;
using FoodDelivery.OrderApi.DTOs;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent(long OrderId, int BranchId, List<OrderItem> OrderItems) : IntegrationEvent;