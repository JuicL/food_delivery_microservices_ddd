using FoodDelibery.EventBus.Events;

namespace FoodDelibery.PaymentProcessor.IntegrationEvents.Events;

public record OrderPaymentSucceededIntegrationEvent(long OrderId) : IntegrationEvent;
