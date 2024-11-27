namespace ProductCatalog.Domain.Entities;

public class Product : BaseEntity
{    
    public string Name { get; set; }
    public decimal Price { get; set; }

    
    public Product(int id, string name, decimal price, decimal? discountedPrice = null)
        : base(id)
    {
        Name = name;
        Price = price;
    }
}
