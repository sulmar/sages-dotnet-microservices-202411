using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

// Interfejs generyczny
public interface IEntityRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> GetById(TKey id);
}


public interface IEntityRepository<T> : IEntityRepository<T, int>
    where T : BaseEntity<int>
{
}
