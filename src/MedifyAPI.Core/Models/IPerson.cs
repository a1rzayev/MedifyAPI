namespace MedifyAPI.Core.Models;

public interface IPerson
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Surname { get; set; }
    DateTime Birthdate { get; set; }
    GenderEnum Gender { get; set; }
    public string Email { get; set; }
    string PhoneNumber { get; set; }
}
