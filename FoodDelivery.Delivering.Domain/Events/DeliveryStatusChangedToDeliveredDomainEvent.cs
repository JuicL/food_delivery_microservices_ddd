using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToDeliveredDomainEvent(long DeliveryId) : INotification;
}