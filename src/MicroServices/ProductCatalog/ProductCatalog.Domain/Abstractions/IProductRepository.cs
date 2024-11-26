using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Abstractions;

public interface IProductRepository : IEntityRepository<Product>
{
    Task<IEnumerable<Product>> GetByPrice(decimal from, decimal to);
}
