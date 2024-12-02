using MedifyAPI.Core.Models;

namespace MedifyAPI.Infrastructure.Models;

public class Doctor : IPerson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public GenderEnum Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; } 
    public DateTime DateJoined { get; set; } 
    public SpecialityEnum Speciality { get; set; }
}
