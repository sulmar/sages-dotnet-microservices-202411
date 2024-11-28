using FastEndpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using System.Security.Claims;

namespace ProductCatalog.Api.Endpoints.Products;

public class GetAll(IProductRepository repository) : EndpointWithoutRequest<IEnumerable<Product>>
{
    public override void Configure()
    {
        //AllowAnonymous();
        Get("/api/products");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var identity = this.User.Identity as ClaimsIdentity;        

        var products = await repository.GetAll();

        Response = products;
    }
}
