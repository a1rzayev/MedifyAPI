using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Services;

public interface IDoctorService
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(Guid id);
    Task<Doctor> AddAsync(Doctor doctor);
    Task<Doctor> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(Guid id);
}