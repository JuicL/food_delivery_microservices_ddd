using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToWaitingReceiveIntegrationEvent(long DeliveryId) : IntegrationEvent;
}