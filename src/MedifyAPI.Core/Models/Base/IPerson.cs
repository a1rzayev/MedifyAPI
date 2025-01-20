using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Models.Base;

public interface IPerson
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Surname { get; set; }
    DateTime Birthdate { get; set; }
    GenderEnum Gender { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    public string Password { get; set; }
    DateTime DateJoined { get; set; } 
}
