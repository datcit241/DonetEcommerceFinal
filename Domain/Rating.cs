namespace Domain;

public class Rating
{
    public string UserId { get; set; }
    public User User { get; set; }
    public Guid OrderDetailsId { get; set; }
    public OrderDetails OrderDetails { get; set; }
    public int Value { get; set; }
    public string Message { get; set; }
}