using MedifyAPI.Core.Models;

namespace MedifyAPI.Infrastructure.Models;

public class Log : ILog
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

