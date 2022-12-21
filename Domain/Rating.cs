using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Rating
{
    public string UserId { get; set; }
    public User User { get; set; }
    public Guid CustomerOrderId { get; set; }
    public CustomerOrder CustomerOrder { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    [Range(1, 5, ErrorMessage = "Rating should be between 1 and 5")]
    public int Value { get; set; }

    public string? Message { get; set; }
}