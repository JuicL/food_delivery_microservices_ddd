using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.OrderApi.Infrastructure.Repository.Implementation
{
    public class OrderRequestRepository : IOrderRequestRepository
    {
        private readonly OrderingContext _orderingContext;

        public OrderRequestRepository(OrderingContext orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public IUnitOfWork UnitOfWork => _orderingContext;

        public async Task<OrderRequest> CreateAsync(OrderRequest request)
        {
            var entity =  await _orderingContext.OrderRequests.AddAsync(request);
            return entity.Entity;
        }

        public async Task<OrderRequest> GetAsync(long id)
        {
            return await _orderingContext.OrderRequests.Where(x => x.Id == id).Include(x=> x.Dishes).FirstOrDefaultAsync();
        }

        public Task<OrderRequest> UpdateAsync(OrderRequest request)
        {
            return Task.FromResult(_orderingContext.OrderRequests.Update(request).Entity);
        }
    }
}
