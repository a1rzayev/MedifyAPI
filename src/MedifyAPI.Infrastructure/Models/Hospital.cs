using MedifyAPI.Core.Models;

namespace MedifyAPI.Infrastructure.Models;

public class Hospital
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public IEnumerable<string> Phones { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Dictionary<WeekDayEnum, (TimeOnly, TimeOnly)> WorkingDaysHours { get; set; }
    public HospitalTypeEnum Type { get; set; }
}
