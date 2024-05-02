using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantCatalogContext db;

        public RestaurantRepository(RestaurantCatalogContext context)
        {
            db = context;
        }

        public IUnitOfWork UnitOfWork => db;

        public async Task<Restaurant> CreateAsync(Restaurant restaurant, CancellationToken cancellationToken)
        {
            var entity =  await db.Restaurants.AddAsync(restaurant);
            return entity.Entity;
        }

        public async Task<Restaurant> FindByIdAsync(long id, CancellationToken cancellationToken)
        {
            var restaurant = await db.Restaurants.Where(x => x.Id == id).SingleOrDefaultAsync();
            if(restaurant is not null)
            {
                await db.Entry(restaurant).Collection(x => x.Branches).LoadAsync();
            }
            return restaurant;
        }

        public async Task<IReadOnlyCollection<Restaurant>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Restaurants.Include(x=> x.Branches).ToListAsync();
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken)
        {
            return await Task.FromResult(db.Restaurants.Update(restaurant).Entity);
        }
    }
}
