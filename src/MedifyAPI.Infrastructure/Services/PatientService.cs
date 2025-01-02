using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;

namespace MedifyAPI.Infrastructure.Services;


public class PatientService : IPatientService
{
    private readonly IPatientRepository patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }
    

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await patientRepository.GetAllAsync();
    }

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await patientRepository.GetByIdAsync(id);
    }

    public async Task<Patient> AddAsync(Patient patient)
    {
        return await patientRepository.AddAsync(patient);
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        return await patientRepository.UpdateAsync(patient);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await patientRepository.DeleteAsync(id);
    }
}
