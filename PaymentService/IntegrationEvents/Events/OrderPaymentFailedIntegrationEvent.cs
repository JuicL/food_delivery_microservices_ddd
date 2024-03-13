using FoodDelibery.EventBus.Events;

namespace FoodDelibery.PaymentProcessor.IntegrationEvents.Events;

public record OrderPaymentFailedIntegrationEvent(long OrderId) : IntegrationEvent;
