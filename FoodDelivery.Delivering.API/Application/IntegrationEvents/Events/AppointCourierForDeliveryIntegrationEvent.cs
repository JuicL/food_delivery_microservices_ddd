using FoodDelibery.EventBus.Events;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record AppointCourierForDeliveryIntegrationEvent(
        long CourierId,
        long DeliveryId,
        string SenderAddress,
        string DeliveryAddress) : IntegrationEvent;
}
