using DDD.Domain.Contracts;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

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

        public async Task<List<Courier>> GellAllFreeAsync()
        {
            return await _deliveryContext.Couriers.Where(x => x.WorkStatus == WorkStatus.AtWork).ToListAsync();
        }

        public async Task<List<Courier>> GellAllFreeNearestAsync(Point point, double? diameters = null)
        {
            if (diameters is not null)
            {
                return await _deliveryContext.Couriers.Where(x => x.WorkStatus == WorkStatus.AtWork)
                    .Where(x => x.Location.Distance(point) < diameters).ToListAsync();
            }

            return await _deliveryContext.Couriers.Where(x => x.WorkStatus == WorkStatus.AtWork)
                    .OrderBy(c => c.Location.Distance(point))
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
