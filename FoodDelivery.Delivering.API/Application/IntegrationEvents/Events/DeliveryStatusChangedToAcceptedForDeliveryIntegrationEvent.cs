using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToAcceptedForDeliveryIntegrationEvent(long DeliveryId) : IntegrationEvent;
}