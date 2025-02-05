using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Core.Models;

public class Log : ILog
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}

