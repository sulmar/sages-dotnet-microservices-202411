using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Infrastructure;

public class FakeShoppingCartRepository : IShoppingCartRepository
{
    private Cart Cart = new Cart(); 

    public Task Add(CartItem item)
    {
        Cart.Items.Add(item);

        return Task.CompletedTask;
    }
}


