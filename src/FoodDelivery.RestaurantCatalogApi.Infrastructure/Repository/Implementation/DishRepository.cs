
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation
{
    public class DishRepository : IDishRepository
    {
        private readonly RestaurantCatalogContext db;

        public IUnitOfWork UnitOfWork => db;

        public DishRepository(RestaurantCatalogContext db)
        {
            this.db = db;
        }

        public async Task<Dish> CreateAsync(Dish dish, CancellationToken cancellationToken)
        {
            var entity = await db.Dishes.AddAsync(dish);
            return entity.Entity;
        }
        public async Task<Dish> UpdateAsync(Dish dish, CancellationToken cancellationToken)
        {
            return await Task.FromResult(db.Dishes.Update(dish).Entity);
        }

        public async Task<Dish> FindById(long id, CancellationToken cancellationToken)
        {
            return await db.Dishes.Where(re => re.Id == id).Include(x=> x.DishType).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Dish>> GetAllDichesAsync(CancellationToken cancellationToken)
        {
            return await db.Dishes.Include(x => x.DishType).ToListAsync();
        }
    }
}
