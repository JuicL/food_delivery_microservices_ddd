using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record DeliveryStatusChangedToAcceptedForDeliveryDomainEvent(long DeliveryId) : INotification;
}