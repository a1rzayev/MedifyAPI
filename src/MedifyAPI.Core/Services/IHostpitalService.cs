using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Services;

public interface IHospitalService
{
    Task<IEnumerable<Hospital>> GetAllAsync();
    Task<Hospital?> GetByIdAsync(Guid id);
    Task<Hospital> AddAsync(Hospital Hospital);
    Task<Hospital> UpdateAsync(Hospital Hospital);
    Task<bool> DeleteAsync(Guid id);
}