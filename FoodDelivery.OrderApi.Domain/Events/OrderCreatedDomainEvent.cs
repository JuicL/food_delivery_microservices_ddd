using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.ValueObjects;
using MediatR;

namespace FoodDelivery.OrderApi.Domain.Events
{
    public record class OrderCreatedDomainEvent(
        OrderRequest order,
        int userId, 
        string userName,
        Phone Phone
        ) : INotification
    {
        
    
    }
}