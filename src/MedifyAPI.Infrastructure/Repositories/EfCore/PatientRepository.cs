using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class PatientRepository : IPatientRepository
{
    private readonly MedifyDbContext _context;

    public PatientRepository(MedifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetPatientByIdAsync(Guid id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> AddPatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> DeletePatientAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<IEnumerable<Patient>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Patient?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> AddAsync(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<Patient> UpdateAsync(Patient patient)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
