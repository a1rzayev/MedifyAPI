using MedifyAPI.Core.Enums;
using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Core.Models;

public class Hospital
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public IEnumerable<string> Phones { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public HospitalTypeEnum Type { get; set; }
}
