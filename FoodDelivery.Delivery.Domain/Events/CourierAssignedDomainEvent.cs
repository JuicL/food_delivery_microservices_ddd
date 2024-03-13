using MediatR;

namespace FoodDelivery.Delivery.Domain.Events
{
    public record CourierAssignedDomainEvent(long DeliveryId) : INotification;

}