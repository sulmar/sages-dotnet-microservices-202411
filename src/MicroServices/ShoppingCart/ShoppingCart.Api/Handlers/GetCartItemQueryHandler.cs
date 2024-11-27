using MediatR;
using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Queries;

namespace ShoppingCart.Api.Handlers;

public class GetCartItemQueryHandler(IShoppingCartRepository repository) : IRequestHandler<GetCartItemQuery, CartItem>
{
    public async Task<CartItem> Handle(GetCartItemQuery request, CancellationToken cancellationToken)
    {
        return await repository.Get(request.id);        
    }
}
