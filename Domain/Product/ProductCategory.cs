namespace Domain;

public class ProductCategory
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    // public int CategoryId { get; set; }
    // public Category Category { get; set; }
    public int CategoryValueId { get; set; }
    public CategoryValue CategoryValue { get; set; }
}