using DDD.Domain.Contracts;

namespace FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate
{
    public interface IAssignDeliveryRepository : IRepository<AssignDelivery>
    {
        Task<AssignDelivery> CreateAsync(AssignDelivery assignDelivery);
        Task<AssignDelivery> UpdateAsync(AssignDelivery assignDelivery);
        Task<AssignDelivery> GetByIdAsync(long id);
        Task<AssignDelivery> GetByCourierAndDeliveryIdsAsync(long deliveryId, long courierId);
    }
}
