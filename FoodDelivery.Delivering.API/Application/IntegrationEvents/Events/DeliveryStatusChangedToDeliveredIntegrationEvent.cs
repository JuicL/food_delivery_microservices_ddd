using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToDeliveredIntegrationEvent(long DeliveryId,long OrderId) : IntegrationEvent;
}