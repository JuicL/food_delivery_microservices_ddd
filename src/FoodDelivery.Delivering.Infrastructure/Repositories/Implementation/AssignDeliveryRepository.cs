using DDD.Domain.Contracts;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
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
            return await _deliveryContext.AssignDeliveries.Where(x => x.Id == id)
                .Include(x => x.Delivery)
                .Include(x => x.Courier)
                .SingleOrDefaultAsync();
        }

        public async Task<AssignDelivery> GetByCourierAndDeliveryIdsAsync(long deliveryId, long courierId)
        {
            return await _deliveryContext.AssignDeliveries
                .Where(x => x.DeliveryId == deliveryId)
                .Where(x=> x.CourierId == courierId)
                .Include(x=> x.Delivery)
                .Include(x=> x.Courier)
                .SingleOrDefaultAsync();

        }

        public Task<AssignDelivery> UpdateAsync(AssignDelivery assignDelivery)
        {
            return Task.FromResult(_deliveryContext.AssignDeliveries.Update(assignDelivery).Entity);
        }

        public async Task<List<AssignDelivery>> GetByCourierIdAsync(long courierId)
        {
            return await _deliveryContext.AssignDeliveries
                .Where(x => x.CourierId == courierId)
                .Include(x => x.Delivery)
                .ToListAsync();
        }

        public async Task<List<AssignDelivery>> GetByCourierIdAndStatusAsync(long courierId, AssignDeliveryStatus status)
        {
            return await _deliveryContext.AssignDeliveries
                .Where(x => x.CourierId == courierId)
                .Where(x=> x.Status == status)
                .ToListAsync();
        }
    }
}
