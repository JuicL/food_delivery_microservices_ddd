using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.Domain.Events
{
    public record DeliveryStatusChangedToDeliveredDomainEvent(Delivery Delivery) : INotification;
}