using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToAcceptedForDeliveryDomainEvent(long DeliveryId) : INotification;
}