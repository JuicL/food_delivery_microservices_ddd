using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderStatusChangedToDeliveredIntegrationEvent(long OrderId) : IntegrationEvent;