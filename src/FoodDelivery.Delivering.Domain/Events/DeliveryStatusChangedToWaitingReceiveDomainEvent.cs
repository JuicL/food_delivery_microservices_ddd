using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToWaitingReceiveDomainEvent(long DeliveryId) : INotification;
}