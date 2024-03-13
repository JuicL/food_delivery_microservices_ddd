using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record DeliveryStatusChangedToWaitingReceiveDomainEvent(long DeliveryId) : INotification;
}