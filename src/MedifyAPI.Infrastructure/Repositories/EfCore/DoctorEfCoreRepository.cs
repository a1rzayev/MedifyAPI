using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class DoctorEfCoreRepository : IDoctorRepository
{
    private readonly MedifyDbContext _context;

    public DoctorEfCoreRepository(MedifyDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor> UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null) return false;

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task SetValidation(Guid id, bool value){
        var uservalidation = await _context.UserValidations.FindAsync(id);
        if(uservalidation == null) 
            await _context.UserValidations.AddAsync(new UserValidation{ UserId = id, IsValidated = value});
        else 
            uservalidation.IsValidated = value;
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> GetByEmailAsync(string email)
    {
        return await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Email == email);
    }
}
