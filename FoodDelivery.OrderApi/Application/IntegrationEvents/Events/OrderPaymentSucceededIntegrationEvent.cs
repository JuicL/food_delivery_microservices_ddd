using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderPaymentSucceededIntegrationEvent(long OrderId) : IntegrationEvent;

}
