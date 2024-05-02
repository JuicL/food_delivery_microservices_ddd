using FoodDelivery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToAwaitingValidationIntegrationEvent(long orderId,int branchId, List<int> dishesId)
        {
            OrderId = orderId;
            BranchId = branchId;
            DishesId = dishesId;
        }
        public long OrderId { get; }
        public int BranchId { get; }
        public List<int> DishesId { get; }
    }
    

}
