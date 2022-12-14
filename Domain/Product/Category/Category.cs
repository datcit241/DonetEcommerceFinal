namespace Domain;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CategoryValue> CategoryValues { get; set; }
    public ICollection<ProductCategory> Products { get; set; }
}