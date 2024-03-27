using FoodDelivery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record class OrderAvailabilityConfirmedIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; }

        public OrderAvailabilityConfirmedIntegrationEvent(long orderId)
        {
            OrderId = orderId;
        }
    }
}
