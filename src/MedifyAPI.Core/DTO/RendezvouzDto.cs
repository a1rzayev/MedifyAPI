using System.ComponentModel.DataAnnotations;

namespace MedifyAPI.Core.DTO;
public class RendezvouzDto
{
    [Required(ErrorMessage = "SenderId is required.")]
    public Guid SenderId { get; set; }
    [Required(ErrorMessage = "ReceiverId is required.")]
    public  Guid ReceiverId { get; set; }
    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }
}
