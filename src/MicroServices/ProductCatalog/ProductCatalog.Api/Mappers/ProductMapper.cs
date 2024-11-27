using ProductCatalog.Api.DTOs;
using ProductCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProductCatalog.Api.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductDTO Map(Product product);
}

