using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryCreatedIntegrationEvent(long DeliveryId) : IntegrationEvent;
}
