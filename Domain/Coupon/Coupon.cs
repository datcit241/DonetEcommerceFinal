using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Coupon
{
    public Guid Id { get; set; }
    public string Code { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must not be negative")]
    public int Quantity { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must not be negative")]
    public double Amount { get; set; }

    public ICollection<CouponUser> Users { get; set; }
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
}