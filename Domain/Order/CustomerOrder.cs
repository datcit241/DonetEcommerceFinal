namespace Domain;

public class CustomerOrder
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public User User { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public Coupon? Coupon { get; set; }
}