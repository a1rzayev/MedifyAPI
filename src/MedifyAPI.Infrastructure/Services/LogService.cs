using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;

namespace MedifyAPI.Infrastructure.Services;


public class LogService : ILogService
{
    private readonly ILogRepository logRepository;

    public LogService(ILogRepository logRepository)
    {
        this.logRepository = logRepository;
    }
    

    public async Task<IEnumerable<Log>> GetAllAsync()
    {
        return await logRepository.GetAllAsync();
    }

    public async Task<Log?> GetByIdAsync(Guid id)
    {
        return await logRepository.GetByIdAsync(id);
    }

    public async Task<Log> AddAsync(Log log)
    {
        return await logRepository.AddAsync(log);
    }

    public async Task<Log> UpdateAsync(Log log)
    {
        return await logRepository.UpdateAsync(log);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await logRepository.DeleteAsync(id);
    }
}
