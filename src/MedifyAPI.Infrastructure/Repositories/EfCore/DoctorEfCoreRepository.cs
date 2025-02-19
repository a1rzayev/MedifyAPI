using MedifyAPI.Core.DTO;
using MedifyAPI.Core.Enums;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Requests;
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

    public async Task<IEnumerable<Doctor>> GetAllValidatedAsync()
    {
        var validatedDoctorIds = await _context.DoctorValidations
                                .Where(d => d.IsValidated)
                                .Select(d => d.UserId)
                                .ToListAsync();

        return await _context.Doctors
         .Where(d => validatedDoctorIds.Contains(d.Id))
         .ToListAsync();
    }

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
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
    public async Task SetValidation(Guid id, bool value)
    {
        var uservalidation = await _context.UserValidations.FindAsync(id);
        if (uservalidation == null)
            await _context.UserValidations.AddAsync(new UserValidation(id, value));
        else
            uservalidation.IsValidated = value;
        await _context.SaveChangesAsync();
    }
    public async Task VerifyDegreeRequestAsync(Guid id)
    {
        await _context.VerifyDegreeRequests.AddAsync(new VerifyDegreeRequest(id));
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasPendingRequestAsync(Guid id)
    {
        return await _context.VerifyDegreeRequests.AnyAsync(vr => vr.SenderId == id);
    }

    public async Task<bool> IsValidated(Guid id)
    {
        var userValidation = await _context.UserValidations.FindAsync(id);
        return userValidation.IsValidated;
    }

    public async Task<IEnumerable<VerifyDegreeRequest>?> GetAllVerifyDegreeRequestAsync()
    {
        return await _context.VerifyDegreeRequests.ToListAsync();
    }

    public async Task<Doctor?> GetByEmailAsync(string email)
    {
        return await _context.Doctors.FirstOrDefaultAsync(doctor => doctor.Email == email);
    }

    public async Task ApproveDegreeAsync(Guid requestId)
    {
        var degreeRequest = await _context.VerifyDegreeRequests.FindAsync(requestId);
        if (degreeRequest == null)
        {
            throw new KeyNotFoundException("Degree request not found.");
        }
        await this.SetValidation(degreeRequest.SenderId, true);
        degreeRequest.State = RequestStateEnum.Approved;
        _context.VerifyDegreeRequests.Remove(degreeRequest);
        await _context.SaveChangesAsync();
    }


    public async Task DenyDegreeAsync(Guid requestId)
    {
        var degreeRequest = await _context.VerifyDegreeRequests.FindAsync(requestId);
        if (degreeRequest == null)
        {
            throw new KeyNotFoundException("Degree request not found.");
        }
        await this.SetValidation(degreeRequest.SenderId, false);
        degreeRequest.State = RequestStateEnum.Denied;
        _context.VerifyDegreeRequests.Remove(degreeRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RendezvouzRequest>> GetAllRendezvouzRequestsAsync(Guid id)
    {
        var requests = await _context.RendezvouzRequests
        .Where(rq => rq.DoctorId == id)
        .ToListAsync();
        return requests;
    }
}
