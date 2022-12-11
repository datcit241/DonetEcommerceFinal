namespace Domain;

public class Rating
{
    public User User { get; set; }
    public OrderDetails OrderDetails { get; set; }
    public int Value { get; set; }
}