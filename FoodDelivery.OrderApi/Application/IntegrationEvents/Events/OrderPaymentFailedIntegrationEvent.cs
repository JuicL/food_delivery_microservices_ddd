using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderPaymentFailedIntegrationEvent(long OrderId) : IntegrationEvent;
