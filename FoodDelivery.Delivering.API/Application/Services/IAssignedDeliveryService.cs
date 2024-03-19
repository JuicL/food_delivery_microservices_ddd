using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public interface IAssignedDeliveryService
    {
        Task AssignDeliveryToCourier(long DeliveryId, long CourierId);
        Task<List<Courier>> GetCouriersForDelivery(Delivery delivery);
    }
}
