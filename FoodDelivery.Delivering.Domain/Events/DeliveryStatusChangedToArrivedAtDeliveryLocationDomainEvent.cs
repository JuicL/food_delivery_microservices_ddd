using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToDeliveredLocationDomainEvent(long DeliveryId) : INotification;
}