using Microsoft.EntityFrameworkCore.Storage;

namespace FoodDelivery.RestaurantCatalogApi.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> StartTransaction(CancellationToken token = default);
        
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}