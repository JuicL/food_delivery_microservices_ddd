using FoodDelivery.EventBus.Events;

namespace FoodDelivery.PaymentProcessor.IntegrationEvents.Events;

public record OrderPaymentFailedIntegrationEvent(long OrderId) : IntegrationEvent;
