using DDD.Domain.Contracts;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Delivering.Infrastructure.Repositories.Implementation
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DeliveryContext _deliveryContext;

        public DeliveryRepository(DeliveryContext deliveryContext)
        {
            _deliveryContext = deliveryContext;
        }

        public IUnitOfWork UnitOfWork => _deliveryContext;

        public async Task<Delivery> CreateAsync(Delivery delivery)
        {
            var entity = await _deliveryContext.Deliveries.AddAsync(delivery);
            return entity.Entity;
        }

        public async Task<List<Delivery>> GetActivesByCourierIdAsync(long id)
        {
            return await _deliveryContext.Deliveries.Where(x => x.CourierId == id)
                .Where(x=> x.DeliveryStatus != DeliveryStatus.Created 
                && x.DeliveryStatus != DeliveryStatus.Delivered
                && x.DeliveryStatus != DeliveryStatus.Canceled)
                .ToListAsync();
        }

        public async Task<List<Delivery>> GetAllAsync()
        {
            return await _deliveryContext.Deliveries.ToListAsync();
        }

        public async Task<List<Delivery>> GetByCourierIdAsync(long id)
        {
            return await _deliveryContext.Deliveries.Where(x=> x.CourierId == id).ToListAsync();
        }

        public async Task<Delivery> GetByIdAsync(long id)
        {
            return await _deliveryContext.Deliveries.Where(x => x.RecipientId == id).SingleOrDefaultAsync();
        }

        public async Task<List<Delivery>> GetByUserIdAsync(long id)
        {
            return await _deliveryContext.Deliveries.Where(x => x.RecipientId == id).ToListAsync();
        }

        public Task<Delivery> UpdateAsync(Delivery delivery)
        {
            return Task.FromResult(_deliveryContext.Deliveries.Update(delivery).Entity);
        }
    }
}
