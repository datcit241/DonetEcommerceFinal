using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Bio { get; set; }
    public Image? Avatar { get; set; }
    public Cart Cart { get; set; }
    public decimal TotalValueOfPurchases { get; set; }
    public ICollection<CustomerOrder> OrderHistory { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<CouponUser> Coupons { get; set; }
}