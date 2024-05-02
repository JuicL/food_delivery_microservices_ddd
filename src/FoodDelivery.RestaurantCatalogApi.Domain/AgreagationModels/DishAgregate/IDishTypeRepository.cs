namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public interface IDishTypeRepository 
    {
        Task<IReadOnlyCollection<DishType>> GetAll(CancellationToken cancellationToken);
        Task<DishType> GetByName(string name,CancellationToken cancellationToken);

    }
}
