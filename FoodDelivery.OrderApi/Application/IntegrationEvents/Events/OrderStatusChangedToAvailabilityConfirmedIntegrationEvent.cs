using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderStatusChangedToAvailabilityConfirmedIntegrationEvent(long OrderId,decimal totalPrice) : IntegrationEvent;
