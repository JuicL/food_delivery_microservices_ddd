using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public class OrderStatusChangedToAvailabilityConfirmedDomainEvent : INotification
    {
        public OrderStatusChangedToAvailabilityConfirmedDomainEvent(long ordeId)
        {
            OrdeId = ordeId;
        }

        public long OrdeId { get; }
    }
}