namespace Domain;

public class Coupon
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    public ICollection<CouponUser> Users { get; set; }
}