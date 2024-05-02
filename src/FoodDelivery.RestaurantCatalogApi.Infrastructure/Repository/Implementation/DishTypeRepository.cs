
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation
{
    public class DishTypeRepository : IDishTypeRepository
    {
        private readonly RestaurantCatalogContext _context;

        public DishTypeRepository(RestaurantCatalogContext context)
        {
            _context = context;
        }

        public Task<IReadOnlyCollection<DishType>> GetAll(CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.DishTypes.ToListAsync() as IReadOnlyCollection<DishType>);
        }

        public Task<DishType> GetByName(string name, CancellationToken cancellationToken)
        {
           return _context.DishTypes.FirstOrDefaultAsync(x=> x.Name == name);
        }
    }
}
