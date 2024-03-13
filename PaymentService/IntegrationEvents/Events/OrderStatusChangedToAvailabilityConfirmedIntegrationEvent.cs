using FoodDelibery.EventBus.Events;

namespace FoodDelibery.PaymentProcessor.IntegrationEvents.Events;

public record OrderStatusChangedToAvailabilityConfirmedIntegrationEvent(long OrderId, decimal totalPrice) : IntegrationEvent;
