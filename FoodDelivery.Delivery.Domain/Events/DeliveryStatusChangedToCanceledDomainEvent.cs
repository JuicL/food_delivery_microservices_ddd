using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record DeliveryStatusChangedToCanceledDomainEvent(long OrderId) : INotification;
}