namespace Domain;

public class CouponUser
{
    public Guid CouponId { get; set; }
    public Coupon Coupon { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
}