using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class LogEfCoreRepository : ILogRepository
{
    private readonly MedifyDbContext _context;

    public LogEfCoreRepository(MedifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Log>> GetAllAsync()
    {
        return await _context.Logs.ToListAsync();
    }

    public async Task<Log?> GetByIdAsync(Guid id)
    {
        return await _context.Logs.FindAsync(id);
    }

    public async Task<Log> AddAsync(Log log)
    {
        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
        return log;
    }

    public async Task<Log> UpdateAsync(Log log)
    {
        _context.Logs.Update(log);
        await _context.SaveChangesAsync();
        return log;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var Log = await _context.Logs.FindAsync(id);
        if (Log == null) return false;

        _context.Logs.Remove(Log);
        await _context.SaveChangesAsync();
        return true;
    }
}
