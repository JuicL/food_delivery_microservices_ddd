using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record CourierAssignedIntegrationEvent(long DeliveryId,long CourierId) : IntegrationEvent;
}
