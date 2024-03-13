using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public interface IOrderRequestRepository : IRepository<OrderRequest>
    {
        public Task<OrderRequest> CreateAsync(OrderRequest request);
        public Task<OrderRequest> UpdateAsync(OrderRequest request);
        public Task<OrderRequest> GetAsync(long id);
    }
}
