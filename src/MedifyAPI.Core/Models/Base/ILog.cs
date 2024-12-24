namespace MedifyAPI.Core.Models.Base;

public interface ILog
{
    Guid Id { get; set; }
    string Description { get; set; }
    DateTime Date { get; set; }
}

