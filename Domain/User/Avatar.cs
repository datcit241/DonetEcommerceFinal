using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Avatar : Image
{
    public User User { get; set; }
}