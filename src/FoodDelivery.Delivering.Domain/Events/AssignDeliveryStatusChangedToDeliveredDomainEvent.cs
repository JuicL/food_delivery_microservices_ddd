using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record AssignDeliveryStatusChangedToDeliveredDomainEvent(AssignDelivery assignDelivery) : INotification;

}