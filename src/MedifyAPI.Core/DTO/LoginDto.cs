using System.ComponentModel.DataAnnotations;

namespace MedifyAPI.Core.DTO;
public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(100)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).+$", ErrorMessage = "Password must contain at least one uppercase letter and one special character.")]  
    public string? Password { get; set; }
}