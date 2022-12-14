namespace Domain;

public class CategoryValue
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductCategory> Products { get; set; }
}