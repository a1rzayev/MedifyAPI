using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Repositories;

public interface ILogRepository
{
    Task<IEnumerable<Log>> GetAllAsync();
    Task<Log?> GetByIdAsync(Guid id);
    Task<Log> AddAsync(Log log);
    Task<Log> UpdateAsync(Log log);
    Task<bool> DeleteAsync(Guid id);
}