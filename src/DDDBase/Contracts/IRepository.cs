using DDD.Domain.Models;

namespace DDD.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

    }
}