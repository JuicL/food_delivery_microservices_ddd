using FoodDelivery.EventBus.Events;

namespace FoodDelivery.PaymentProcessor.IntegrationEvents.Events;

public record OrderStatusChangedToAvailabilityConfirmedIntegrationEvent(long OrderId, decimal totalPrice) : IntegrationEvent;
