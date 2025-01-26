using System.ComponentModel.DataAnnotations;

namespace MedifyAPI.Core.DTO;
public class SignupDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    [MaxLength(50, ErrorMessage = "Surname can't be longer than 50 characters.")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(100)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\W)(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one special character, and one number.")]
    public string? Password { get; set; }

    public string? Role {get; set;}
}
