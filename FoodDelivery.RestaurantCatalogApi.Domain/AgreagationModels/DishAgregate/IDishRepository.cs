using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public interface IDishRepository : IRepository<Dish>
    {
        Task<Dish> CreateAsync(Dish dish, CancellationToken cancellationToken);
        Task<Dish> UpdateAsync(Dish dish, CancellationToken cancellationToken);
        Task<Dish> FindById(long id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Dish>> GetAllDichesAsync (CancellationToken cancellationToken);

    }
}
