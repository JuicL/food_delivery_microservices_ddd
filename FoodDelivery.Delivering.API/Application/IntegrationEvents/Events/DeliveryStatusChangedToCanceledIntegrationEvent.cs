using FoodDelibery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToCanceledIntegrationEvent(long DeliveryId,long OrderId) : IntegrationEvent;
}