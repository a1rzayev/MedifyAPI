using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class PatientEfCoreRepository : IPatientRepository
{
    private readonly MedifyDbContext _context;

    public PatientEfCoreRepository(MedifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _context.Patients.FindAsync(id);
    }
    public async Task<Patient?> GetByEmailAsync(string email)
    {
        return await _context.Patients.FirstOrDefaultAsync(patient => patient.Email == email);
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return false;
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SetValidation(Guid id, bool value){
        var uservalidation = await _context.UserValidations.FindAsync(id);
        if(uservalidation == null) 
            await _context.UserValidations.AddAsync(new UserValidation( id, value));
        else 
            uservalidation.IsValidated = value;
        await _context.SaveChangesAsync();
    }
}
