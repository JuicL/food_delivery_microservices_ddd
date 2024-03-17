using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public interface IAssignedDeliveryService
    {
        Task<long> AssignDeliveryToCourier(Delivery delivery);
    }
}
