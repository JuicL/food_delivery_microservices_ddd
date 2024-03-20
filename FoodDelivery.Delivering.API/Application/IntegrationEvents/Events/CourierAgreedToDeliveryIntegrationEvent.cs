using FoodDelibery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record CourierAgreedToDeliveryIntegrationEvent(long DeliveryId,long CourierId) : IntegrationEvent;
}
