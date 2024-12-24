using MedifyAPI.Core.Enums;
using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Core.Models;

public class Admin : IPerson
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public GenderEnum Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateJoined { get; set; }
}