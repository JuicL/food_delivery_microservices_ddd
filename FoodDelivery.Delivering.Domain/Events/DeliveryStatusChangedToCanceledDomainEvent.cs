using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToCanceledDomainEvent(long DeliveryId) : INotification;
}