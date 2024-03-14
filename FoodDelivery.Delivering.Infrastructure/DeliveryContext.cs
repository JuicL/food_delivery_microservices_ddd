using DDD.Domain.Contracts;
using FoodDelibery.IntegrationEventLogEF;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Infrastructure.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace FoodDelivery.Delivering.Infrastructure
{
    public class DeliveryContext : DbContext, IUnitOfWork
    {
        public DeliveryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Courier> Couriers { get; set; }

        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.ApplyConfiguration(new DeliveryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CourierEntityTypeConfiguration());
            modelBuilder.UseIntegrationEventLogs();
        }

        public async Task<IDbContextTransaction> StartTransaction(CancellationToken token = default)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            return await SaveChangesAsync(cancellationToken) > 0;
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
