using MediatR;

namespace FoodDelivery.Delivery.Domain.AgregationModels.DeliveryAgregate
{
    public record CourierAssignedDomainEvent(long DeliveryId) : INotification;
    
}