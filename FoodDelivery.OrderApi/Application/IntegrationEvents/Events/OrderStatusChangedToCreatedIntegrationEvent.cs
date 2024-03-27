using FoodDelivery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderStatusChangedToCreatedIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToCreatedIntegrationEvent(long orderId, int userId, string userName)
        {
            OrderId = orderId;
            UserId = userId;
            UserName = userName;
        }

        public long OrderId { get; }
        public int UserId { get; }
        public string UserName { get; }
    }
    

}
