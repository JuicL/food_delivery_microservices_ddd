using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<Branch> CreateAsync(Branch branch,CancellationToken cancellationToken);
        Task<Branch> UpdateAsync(Branch branch,CancellationToken cancellationToken);
        Task<Branch> FindByIdAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Branch>> GetBranchesByResaurantIdAsync(int resaurantId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Branch>> GetAllBranchesAsync(CancellationToken cancellationToken);

    }
}
