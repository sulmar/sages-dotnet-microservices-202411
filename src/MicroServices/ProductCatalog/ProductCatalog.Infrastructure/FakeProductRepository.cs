using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure;

// C# 12 Primary Constructor
public class FakeProductRepository(Context context) : IProductRepository
{
    public Task<IEnumerable<Product>> GetAll() => Task.FromResult<IEnumerable<Product>>(context.Products.Values);
    public Task<Product> GetById(int id) => Task.FromResult(context.Products[id]);
    public Task<IEnumerable<Product>> GetByPrice(decimal from, decimal to)
    {
        throw new NotImplementedException();
    }
}
