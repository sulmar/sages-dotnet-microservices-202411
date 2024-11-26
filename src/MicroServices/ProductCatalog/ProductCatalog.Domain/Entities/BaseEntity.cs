namespace ProductCatalog.Domain.Entities;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }

    protected BaseEntity(TKey id)
    {
        this.Id = id;
    }
}


public abstract class BaseEntity : BaseEntity<int>
{
    protected BaseEntity(int id) : base(id)
    {
    }
}
