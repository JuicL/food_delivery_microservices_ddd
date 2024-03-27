using FoodDelivery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderStatusChangedToCanceledIntegrationEvent(long OrderId) : IntegrationEvent;
