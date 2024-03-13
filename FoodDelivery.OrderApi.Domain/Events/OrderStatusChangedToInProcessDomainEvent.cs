using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public record OrderStatusChangedToInProcessDomainEvent(long OrderId) : INotification;
}