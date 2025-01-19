using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Services;

public interface IHospitalService
{
    Task<IEnumerable<Hospital>> GetAllAsync();
    Task<Hospital?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Hospital>?> GetByNameAsync(string name);
    Task<Hospital> AddAsync(Hospital hospital);
    Task<Hospital> UpdateAsync(Hospital hospital);
    Task<bool> DeleteAsync(Guid id);
}