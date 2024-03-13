using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.OrderApi.Infrastructure.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderingContext _orderingContext;

        public UserRepository(OrderingContext orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public IUnitOfWork UnitOfWork => _orderingContext;

        public async Task<User> CreateAsync(User request)
        {
            var entity = await _orderingContext.Users.AddAsync(request);
            return entity.Entity;
        }

        public async Task<User> GetAsync(int id)
        {
            return await _orderingContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _orderingContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();

        }

        public async Task<User> UpdateAsync(User user)
        {
            return await Task.FromResult(_orderingContext.Users.Update(user).Entity);

        }
    }
}
