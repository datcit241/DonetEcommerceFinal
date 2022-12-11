namespace Domain;

public class Rating
{
    public User User { get; set; }
    public Product Product { get; set; }
    public int Value { get; set; }
}