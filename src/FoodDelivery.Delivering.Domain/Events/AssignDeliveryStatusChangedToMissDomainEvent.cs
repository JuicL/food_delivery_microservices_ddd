using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record AssignDeliveryStatusChangedToMissDomainEvent(AssignDelivery assignDelivery) : INotification;

}