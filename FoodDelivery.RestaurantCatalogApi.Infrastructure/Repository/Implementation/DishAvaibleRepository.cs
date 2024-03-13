using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation
{
    public class DishAvaibleRepository : IDishAvaibleRepository
    {
        private readonly RestaurantCatalogContext db;

        public DishAvaibleRepository(RestaurantCatalogContext context)
        {
            db = context;
        }

        public async Task<DishAvaible> CreateAsync(DishAvaible dishAvaible, CancellationToken cancellationToken)
        {
            var entity = await db.DishAvaibles.AddAsync(dishAvaible);
            return entity.Entity;
        }

        public async Task<IReadOnlyCollection<Branch>> GetBranchesByDishIdAsync(int dishId, CancellationToken cancellationToken)
        {
            return await db.DishAvaibles.Where(d => d.DishId == dishId).Select(x=> x.Branch).ToListAsync();
        } 

        public async Task<DishAvaible> GetByIdAsync(int dishId, int branchId, CancellationToken cancellationToken)
        {
            return await db.DishAvaibles.Where(x=> x.BranchId == branchId && x.DishId == dishId)
                .Include(x=> x.Dish)
                .Include(x=> x.Branch)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<DishAvaible>> GetByIdsAsync(List<int> dishes, CancellationToken cancellationToken)
        {
            return await db.DishAvaibles.Where(x => dishes.Contains((int)x.DishId))
                .Include(x => x.Dish)
                .Include(x => x.Branch)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Dish>> GetDishesByBranchIdAsync(int branchId, CancellationToken cancellationToken)
        {
            return await db.DishAvaibles.Where(x => x.BranchId == branchId).Select(x=> x.Dish).ToListAsync();
        }

        public async Task<DishAvaible> UpdateAsync(DishAvaible dishAvaible, CancellationToken cancellationToken)
        {
            return await Task.FromResult(db.DishAvaibles.Update(dishAvaible).Entity);
        }
    }
}
