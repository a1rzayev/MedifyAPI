using System.ComponentModel.DataAnnotations;

namespace MedifyAPI.Core.DTO;
public class RendezvouzRequestDto
{
    [Required(ErrorMessage = "dateTime is required.")]
    public DateTime DateTime { get; set; }
    public string? Description{ get; set; }
}
