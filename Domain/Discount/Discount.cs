namespace Domain;

public class Discount
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public DiscountStatus DiscountStatus { get; set; }
    public ICollection<DiscountProducts> DiscountProducts { get; set; }
}