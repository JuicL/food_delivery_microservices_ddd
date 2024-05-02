using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public record OrderStatusChangedToPaidDomainEvent (long OrderId): INotification;
   
}