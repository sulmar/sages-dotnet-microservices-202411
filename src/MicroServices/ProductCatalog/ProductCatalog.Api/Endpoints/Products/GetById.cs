using FastEndpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Api.Endpoints.Products;

public record IdRequest(int id);

public class GetById(IProductRepository repository) : Endpoint<IdRequest, Product>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/api/products/{id}");
    }

    public override async Task HandleAsync(IdRequest request, CancellationToken ct)
    {
        var product = await repository.GetById(id: request.id);

        if (product == null)
            await SendNotFoundAsync();
        else
            await SendOkAsync(product);          
    }
}
