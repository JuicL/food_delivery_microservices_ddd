using DDD.Domain.Contracts;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Delivering.Infrastructure.Repositories.Implementation
{
    public class AssignDeliveryRepository : IAssignDeliveryRepository
    {
        private readonly DeliveryContext _deliveryContext;

        public AssignDeliveryRepository(DeliveryContext deliveryContext)
        {
            _deliveryContext = deliveryContext;
        }

        public IUnitOfWork UnitOfWork => _deliveryContext;

        public async Task<AssignDelivery> CreateAsync(AssignDelivery assignDelivery)
        {
            var entity = await _deliveryContext.AssignDeliveries.AddAsync(assignDelivery);
            return entity.Entity;
        }

        public async Task<AssignDelivery> GetByIdAsync(long id)
        {
            return await _deliveryContext.AssignDeliveries.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<AssignDelivery> GetByCourierAndDeliveryIdsAsync(long deliveryId, long courierId)
        {
            return await _deliveryContext.AssignDeliveries
                .Where(x => x.DeliveryId == deliveryId)
                .Where(x=> x.CourierId == courierId)
                .OrderBy(x=> x.AssignDateTime)
                .SingleOrDefaultAsync();

        }

        public Task<AssignDelivery> UpdateAsync(AssignDelivery assignDelivery)
        {
            return Task.FromResult(_deliveryContext.AssignDeliveries.Update(assignDelivery).Entity);
        }
    }
}
