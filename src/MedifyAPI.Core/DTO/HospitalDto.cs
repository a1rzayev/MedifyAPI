using System.ComponentModel.DataAnnotations;

namespace MedifyAPI.Core.DTO;
public class HospitalDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "At least one phone number is required.")]
    [MinLength(1, ErrorMessage = "At least one phone number is required.")]
    [Phone(ErrorMessage = "One or more phone numbers are not valid.")]
    public IEnumerable<string>? Phones { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
}

