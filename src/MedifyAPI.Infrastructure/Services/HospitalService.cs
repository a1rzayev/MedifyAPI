using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;

namespace MedifyAPI.Infrastructure.Services;


public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository hospitalRepository;

    public HospitalService(IHospitalRepository hospitalRepository)
    {
        this.hospitalRepository = hospitalRepository;
    }
    

    public async Task<IEnumerable<Hospital>> GetAllAsync()
    {
        return await hospitalRepository.GetAllAsync();
    }

    public async Task<Hospital?> GetByIdAsync(Guid id)
    {
        return await hospitalRepository.GetByIdAsync(id);
    }

    public async Task<Hospital> AddAsync(Hospital hospital)
    {
        return await hospitalRepository.AddAsync(hospital);
    }

    public async Task<Hospital> UpdateAsync(Hospital hospital)
    {
        return await hospitalRepository.UpdateAsync(hospital);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await hospitalRepository.DeleteAsync(id);
    }
}
