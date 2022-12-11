using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Cart
{
    [Key] public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; }
}