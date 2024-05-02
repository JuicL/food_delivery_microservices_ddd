using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record AssignDeliveryStatusChangedToInProcessDomainEvent(AssignDelivery AssignDelivery) : INotification;
}