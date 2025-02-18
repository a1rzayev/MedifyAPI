using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Services;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(Guid id);
    Task<Patient?> GetByEmailAsync(string email);
    Task AddAsync(Patient patient);
    Task<Patient> UpdateAsync(Patient patient);
    Task<bool> DeleteAsync(Guid id);
    Task SetValidation(Guid id, bool value);
    Task RendezvouzRequestAsync(Guid DoctorId, Guid PatientId, DateTime dateTime, string? description);
}