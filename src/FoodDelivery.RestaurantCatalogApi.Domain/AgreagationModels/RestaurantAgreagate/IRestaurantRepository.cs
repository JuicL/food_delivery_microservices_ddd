using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using MediatR;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate
{
    public interface IRestaurantRepository: IRepository<Restaurant>
    {
        Task<Restaurant> CreateAsync(Restaurant restaurant, CancellationToken cancellationToken);
        Task<Restaurant> UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken);
        Task<Restaurant> FindByIdAsync(long id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Restaurant>> GetAllAsync(CancellationToken cancellationToken);

    }
}
