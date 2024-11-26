namespace ShoppingCart.Domain.Models;

public record CartItem(int Id, string Name, decimal Price, int Quantity = 1);

public class Cart
{
    public ICollection<CartItem> Items { get; set; } = [];
}
