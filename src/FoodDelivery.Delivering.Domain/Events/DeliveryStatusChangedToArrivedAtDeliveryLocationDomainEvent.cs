using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEvent(long DeliveryId) : INotification;
}