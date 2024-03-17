using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderDeliveredIntegrationEvent(long OrderId) : IntegrationEvent;
}
