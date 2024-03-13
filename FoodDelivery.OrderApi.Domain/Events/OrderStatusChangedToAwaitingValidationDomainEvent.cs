using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public class OrderStatusChangedToAwaitingValidationDomainEvent : INotification
    {
        public long OrderId { get; }
        public List<Dishes> Dishes { get; }

        public OrderStatusChangedToAwaitingValidationDomainEvent(long orderId, List<Dishes> dishes)
        {
            OrderId = orderId;
            Dishes = dishes;
        }

    }
}