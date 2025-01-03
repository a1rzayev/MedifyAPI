using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;

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

    public async Task<Doctor?> GetByIdAsync(Guid id)
    {
        return await doctorRepository.GetByIdAsync(id);
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
}
