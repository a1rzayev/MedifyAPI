using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class HospitalEfCoreRepository : IHospitalRepository
{
    private readonly MedifyDbContext _context;

    public HospitalEfCoreRepository(MedifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hospital>> GetAllAsync()
    {
        return await _context.Hospitals.ToListAsync();
    }

    public async Task<Hospital?> GetByIdAsync(Guid id)
    {
        return await _context.Hospitals.FindAsync(id);
    }
    public async Task<IEnumerable<Hospital>?> GetByNameAsync(string name)
    {
        return await _context.Hospitals.Where(hospital => hospital.Name == name);
    }

    public async Task<Hospital> AddAsync(Hospital hospital)
    {
        _context.Hospitals.Add(hospital);
        await _context.SaveChangesAsync();
        return hospital;
    }

    public async Task<Hospital> UpdateAsync(Hospital hospital)
    {
        _context.Hospitals.Update(hospital);
        await _context.SaveChangesAsync();
        return hospital;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var hospital = await _context.Hospitals.FindAsync(id);
        if (hospital == null) return false;

        _context.Hospitals.Remove(hospital);
        await _context.SaveChangesAsync();
        return true;
    }
}
