namespace MedifyAPI.Core.Models;

public class UserValidation 
{
    public Guid UserId { get; set; }
    public bool IsValidated { get; set; }

    public UserValidation(Guid UserId, bool IsValidated = false){
        this.UserId = UserId;
        this.IsValidated = IsValidated;
    }
}