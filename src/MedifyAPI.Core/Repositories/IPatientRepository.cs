using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(Guid id);
    Task<Patient?> GetByEmailAsync(string email);
    Task<Patient> AddAsync(Patient patient);
    Task<Patient> UpdateAsync(Patient patient);
    Task<bool> DeleteAsync(Guid id);
}