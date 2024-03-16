using DDD.Domain.Contracts;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Delivering.Infrastructure.Repositories.Implementation
{
    public class CourierRepository : ICourierRepository
    {
        private readonly DeliveryContext _deliveryContext;

        public CourierRepository(DeliveryContext deliveryContext)
        {
            _deliveryContext = deliveryContext;
        }

        public IUnitOfWork UnitOfWork => _deliveryContext;

        public async Task<Courier> CreateAsync(Courier courier)
        {
            var entity = await _deliveryContext.Couriers.AddAsync(courier);
            return entity.Entity;
        }

        public async Task<List<Courier>> GellAllFreeAsync(Address address)
        {
            return await _deliveryContext.Couriers.Where(x => x.WorkStatus == WorkStatus.AtWork)
                .Where(x => x.WorkAddress.Country == address.Country && x.WorkAddress.City == address.City)
                .OrderBy(e => 
                    _deliveryContext.Deliveries
                    .Where(x => x.CourierId == e.Id)
                    .Where(x => x.DeliveryStatus == DeliveryStatus.Delivered)
                    .Count())
                .ToListAsync();
        }

        public async Task<List<Courier>> GetAllAsync()
        {
           return await _deliveryContext.Couriers.ToListAsync();
        }

        public async Task<Courier> GetByIdAsync(long id)
        {
            return await _deliveryContext.Couriers.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public Task<Courier> UpdateAsync(Courier courier)
        {
            return Task.FromResult(_deliveryContext.Couriers.Update(courier).Entity);

        }
    }
}
