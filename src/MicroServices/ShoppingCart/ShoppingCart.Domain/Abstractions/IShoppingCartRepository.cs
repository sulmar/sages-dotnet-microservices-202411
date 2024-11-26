using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Abstractions;

public interface IShoppingCartRepository
{
    Task Add(CartItem item);
}
