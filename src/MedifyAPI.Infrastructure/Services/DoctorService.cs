using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;
using System.Data.Common;
using MedifyAPI.Core.Models.Requests;

namespace MedifyAPI.Infrastructure.Services;


public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        this.doctorRepository = doctorRepository;
    }


    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await doctorRepository.GetAllAsync();
    }
    public async Task<IEnumerable<Doctor>> GetAllValidatedAsync()
    {
        return await doctorRepository.GetAllValidatedAsync();
    }
    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await doctorRepository.GetByIdAsync(id);
    }
    public async Task<Doctor?> GetByEmailAsync(string email)
    {
        return await doctorRepository.GetByEmailAsync(email);
    }

    public async Task<Doctor> AddAsync(Doctor Doctor)
    {
        return await doctorRepository.AddAsync(Doctor);
    }

    public async Task<Doctor> UpdateAsync(Doctor Doctor)
    {
        return await doctorRepository.UpdateAsync(Doctor);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await doctorRepository.DeleteAsync(id);
    }

    public async Task SetValidation(Guid id, bool value)
    {
        await doctorRepository.SetValidation(id, value);
    }
    
    public async Task VerifyDegreeRequestAsync(Guid id){
        await doctorRepository.VerifyDegreeRequestAsync(id);
    }

    public async Task<bool> IsValidated(Guid id){
        return await doctorRepository.IsValidated(id);
    }
    public async Task<bool> HasPendingRequestAsync(Guid id){
        return await doctorRepository.HasPendingRequestAsync(id);
    }

    public async Task<IEnumerable<VerifyDegreeRequest>?> GetAllVerifyDegreeRequestAsync(){
        return await doctorRepository.GetAllVerifyDegreeRequestAsync();
    }

    public async Task ApproveDegreeAsync(Guid requestId){
        await doctorRepository.ApproveDegreeAsync(requestId);
    }
    public async Task DenyDegreeAsync(Guid requestId){
         await doctorRepository.DenyDegreeAsync(requestId);
    }

}
