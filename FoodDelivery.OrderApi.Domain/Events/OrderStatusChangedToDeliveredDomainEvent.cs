using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public record OrderStatusChangedToDeliveredDomainEvent(long OrderId) : INotification;
}