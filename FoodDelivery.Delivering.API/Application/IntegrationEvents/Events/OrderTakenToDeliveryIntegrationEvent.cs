using FoodDelibery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderTakenToDeliveryIntegrationEvent(long OrderId,long DeliveryId) : IntegrationEvent;
}
