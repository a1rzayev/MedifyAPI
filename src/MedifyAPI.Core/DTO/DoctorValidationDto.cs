using System.ComponentModel.DataAnnotations;
using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.DTO;
public class DoctorValidationDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthdate { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? DateJoined { get; set; } 
    public SpecialityEnum? Speciality { get; set; }
    
    public Dictionary<string, (TimeSpan start, TimeSpan end)>? WorkDaysHours { get; set; }
}