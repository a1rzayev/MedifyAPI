using MedifyAPI.Core.Enums;
using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Core.Models;

public class Doctor : IUser
{
    public SpecialityEnum? Speciality { get; set; }
}
