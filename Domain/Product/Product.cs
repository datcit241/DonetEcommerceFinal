namespace Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Image> Images { get; set; }
    public double Price { get; set; }
    public ProductLabel? Label { get; set; }
    public int Quantity { get; set; }
    public ICollection<DiscountProduct> Discounts { get; set; }
    public ProductStatus Status { get; set; }
    public int TotalRatingValues { get; set; }
    public int TotalRatings { get; set; }
    public decimal Revenue { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; }
    public ICollection<ProductVariation> Variations { get; set; }
}