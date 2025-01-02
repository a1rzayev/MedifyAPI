using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;

namespace MedifyAPI.Infrastructure.Services;


public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository HospitalRepository;

    public HospitalService(IHospitalRepository HospitalRepository)
    {
        this.HospitalRepository = HospitalRepository;
    }
    

    public async Task<IEnumerable<Hospital>> GetAllAsync()
    {
        return await HospitalRepository.GetAllAsync();
    }

    public async Task<Hospital?> GetByIdAsync(Guid id)
    {
        return await HospitalRepository.GetByIdAsync(id);
    }

    public async Task<Hospital> AddAsync(Hospital hospital)
    {
        return await HospitalRepository.AddAsync(hospital);
    }

    public async Task<Hospital> UpdateAsync(Hospital hospital)
    {
        return await HospitalRepository.UpdateAsync(hospital);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await HospitalRepository.DeleteAsync(id);
    }
}
