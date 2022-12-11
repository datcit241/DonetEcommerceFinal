namespace Domain;

public class DiscountProducts
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid DiscountId { get; set; }
    public Discount Discount { get; set; }
}