using FoodDelivery.EventBus.Events;

namespace FoodDelivery.PaymentProcessor.IntegrationEvents.Events;

public record OrderPaymentSucceededIntegrationEvent(long OrderId) : IntegrationEvent;
