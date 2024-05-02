
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation
{
    public class BranchRepository : IBranchRepository
    {
        private readonly RestaurantCatalogContext db;

        public BranchRepository(RestaurantCatalogContext context)
        {
            db = context;
        }

        public IUnitOfWork UnitOfWork => db;

        public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken)
        {
            var entity = await db.Branches.AddAsync(branch);
            return entity.Entity;
        }

        public async Task<Branch> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await db.Branches.Where(br => br.Id == id)
                .Include(x=> x.Dishes).ThenInclude(x=> x.Dish).ThenInclude(x=> x.DishType)
                .Include(x=> x.Restaurant)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Branch>> GetAllBranchesAsync(CancellationToken cancellationToken)
        {
            return await db.Branches
                .Include(x => x.Restaurant)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Branch>> GetBranchesByResaurantIdAsync(int resaurantId, CancellationToken cancellationToken)
        {
            var branches = await db.Branches.Where(br => br.Restaurant.Id == resaurantId)
                .Include(x => x.Restaurant)
                .ToListAsync();
            return branches.AsReadOnly();
        }

        public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken)
        {
            return await Task.FromResult(db.Branches.Update(branch).Entity);
        }
    }
}
