using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string? Bio { get; set; }
    public Image? Avatar { get; set; }
    public Cart Cart { get; set; }
    public ICollection<CustomerOrder> OrderHistory { get; set; }
    public ICollection<CouponUser> CouponUsers { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}