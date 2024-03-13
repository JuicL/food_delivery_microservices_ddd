using FoodDelibery.IntegrationEventLogEF;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Domain.Contracts;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure
{
    public class RestaurantCatalogContext : DbContext, IUnitOfWork
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<DishAvaible> DishAvaibles { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishType> DishTypes { get; set; }

        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public RestaurantCatalogContext(DbContextOptions<RestaurantCatalogContext> options,IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DishEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DishTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DishAvaibleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BranchEntityTypeConfiguration());
            modelBuilder.UseIntegrationEventLogs();

        }

        public async Task<IDbContextTransaction> StartTransaction(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await SaveChangesAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

}

