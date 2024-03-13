using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record DeliveryStatusChangedToDeliveredDomainEvent(long DeliveryId) : INotification;
}