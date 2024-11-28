using ShoppingCart.Domain.Abstractions;
using ShoppingCart.Domain.Models;
using StackExchange.Redis;

namespace ShoppingCart.Infrastructure;

// dotnet add package StackExchange.Redis
public class RedisShoppingCartRepository(IConnectionMultiplexer connectionMultipexer) : IShoppingCartRepository
{
    private IDatabase db => connectionMultipexer.GetDatabase();

    public async Task Add(CartItem item)
    {
        string sessionId = "hDGqK7";

        string cartKey = $"cart:{sessionId}";
        string field = $"produkt:{item.Id}";

        await db.HashIncrementAsync(cartKey, field, item.Quantity);
    }

    public Task<CartItem> Get(int id)
    {
        throw new NotImplementedException();
    }
}


