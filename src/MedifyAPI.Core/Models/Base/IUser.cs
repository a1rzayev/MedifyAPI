using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Models.Base;

public class IUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime? Birthdate { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? DateJoined { get; set; } 
}
