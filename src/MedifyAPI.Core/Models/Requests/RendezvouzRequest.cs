using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Models.Requests;

public class RendezvouzRequest
{
    public Guid Id { get; set; }
    public Guid DoctorId {get; set;}
    public Guid PatientId {get; set;}
    public DateTime DateTime {get; set;}
    public string? Description {get; set;}
    public RequestStateEnum State {get; set;}

    public RendezvouzRequest(Guid DoctorId, Guid PatientId, DateTime dateTime, string? description) {
        this.Id = Guid.NewGuid();
        this.DoctorId = DoctorId;
        this.PatientId = PatientId;
        this.DateTime = dateTime;
        this.Description = description;
        this.State = RequestStateEnum.Pending;
    }
}