using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record DeliveryStatusChangedToDeliveredLocationDomainEvent(long DeliveryId) : INotification;
}