using MedifyAPI.Core.Enums;


namespace MedifyAPI.Core.DTO.Base;

using System.ComponentModel.DataAnnotations;

public class IUserUpdateDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    [MaxLength(50, ErrorMessage = "Surname can't be longer than 50 characters.")]
    public string? Surname { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid birthdate format.")]
    [Range(typeof(DateTime), "1/1/1900", "1/1/2025", ErrorMessage = "Birthdate should be between 1/1/1900 and 1/1/2025.")]
    public DateTime? Birthdate { get; set; }
    [EnumDataType(typeof(GenderEnum), ErrorMessage = "Invalid gender selection.")]
    public GenderEnum? Gender { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(100)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\W)(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one special character, and one number.")]
    public string Password { get; set; } // Unhashed password
}

