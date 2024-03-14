using DDD.Domain.Contracts;

namespace FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Task<Delivery> CreateAsync(Delivery delivery);
        Task<Delivery> UpdateAsync(Delivery delivery);
        Task<Delivery> GetByIdAsync(long id);
        Task<List<Delivery>> GetByUserIdAsync(long id);
        Task<List<Delivery>> GetByCourierIdAsync(long id);
        Task<List<Delivery>> GetAllAsync();
    }
}
