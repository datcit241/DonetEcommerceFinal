namespace Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Img { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public Discount? Discount { get; set; }
    public ProductStatus Status { get; set; }
}