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

    public Task<IEnumerable<Product>> GetBySearchCriteria(ProductSearchCriteria criteria)
    {
        var query = context.Products.Values.AsQueryable();

        // Paging
        query = query.Skip(criteria.PageIndex * criteria.PageSize).Take(criteria.PageSize);

        if (!string.IsNullOrEmpty(criteria.Name))
            query = query.Where(p => p.Name.Contains(criteria.Name, StringComparison.OrdinalIgnoreCase));

        if (criteria.From.HasValue)
            query = query.Where(p => p.Price >= criteria.From.Value);

        if (criteria.To.HasValue)
            query = query.Where(p => p.Price <= criteria.To.Value);        


        // Sorting
        query = query.OrderBy(p => p.Id);

       // var q = query.Select(p => new { p.Name, p.Price, p.Id });

        var products = query.ToList();

        return Task.FromResult<IEnumerable<Product>>(products);
    }
}
