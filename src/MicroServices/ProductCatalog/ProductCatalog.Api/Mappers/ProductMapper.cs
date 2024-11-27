using ProductCatalog.Api.DTOs;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Mappers;

public class ProductMapper
{
    public ProductDTO Map(Product product)
    {
        ProductDTO dto = new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
        };

        return dto;
    }
}
