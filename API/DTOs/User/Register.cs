using System.ComponentModel.DataAnnotations;

namespace API.DTOs.User;

public class Register
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]{4,8}$", ErrorMessage = "Username should contain only letters and numbers")]
    public string UserName { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}$", ErrorMessage = "Password should be more complex")]
    public string Password { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
}