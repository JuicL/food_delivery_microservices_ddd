using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;


namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate
{
    public interface IDishAvaibleRepository
    {
        Task<DishAvaible> CreateAsync(DishAvaible dishAvaible, CancellationToken cancellationToken);
        Task<DishAvaible> UpdateAsync(DishAvaible dishAvaible, CancellationToken cancellationToken);
        Task<DishAvaible> GetByIdAsync(int dishId, int branchId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<DishAvaible>> GetByIdsAsync(List<int> dishes, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Dish>> GetDishesByBranchIdAsync(int branchId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Branch>> GetBranchesByDishIdAsync(int dishId, CancellationToken cancellationToken);
    
    }
}
