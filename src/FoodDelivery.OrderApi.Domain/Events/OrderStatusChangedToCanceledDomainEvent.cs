using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public record OrderStatusChangedToCanceledDomainEvent(long OrderId) : INotification;
}