using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> CreateAsync(User request);
        public Task<User> UpdateAsync(User request);
        public Task<User> GetAsync(int id);
        public Task<User> GetByUserNameAsync(string userName);
    }
}
