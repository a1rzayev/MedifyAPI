using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Requests;

namespace MedifyAPI.Core.Repositories;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<IEnumerable<Doctor>> GetAllValidatedAsync();
    Task<Doctor?> GetByIdAsync(Guid id);
    Task<Doctor?> GetByEmailAsync(string email);
    Task<Doctor> AddAsync(Doctor doctor);
    Task<Doctor> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(Guid id);
    Task SetValidation(Guid id, bool value);
    Task VerifyDegreeRequestAsync(Guid id);
}