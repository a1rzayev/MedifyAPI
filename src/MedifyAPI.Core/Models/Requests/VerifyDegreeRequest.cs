using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Models.Requests;

public class VerifyDegreeRequest
{
    public Guid Id { get; set; }
    public Guid SenderId {get; set;}
    public RequestStateEnum State {get; set;}

    public VerifyDegreeRequest(Guid SenderId) {
        this.Id = Guid.NewGuid();
        this.SenderId = SenderId;
        this.State = RequestStateEnum.Pending;
    }
}