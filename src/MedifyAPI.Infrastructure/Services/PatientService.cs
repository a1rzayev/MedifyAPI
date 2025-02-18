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
    public async Task<Patient?> GetByEmailAsync(string email)
    {
        return await patientRepository.GetByEmailAsync(email);
    }

    public async Task AddAsync(Patient patient)
    {
        await patientRepository.AddAsync(patient);
    }

    public async Task<Patient> UpdateAsync(Patient patient)
    {
        return await patientRepository.UpdateAsync(patient);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await patientRepository.DeleteAsync(id);
    }

    public async Task SetValidation(Guid id, bool value){
        await patientRepository.SetValidation(id, value);
    }

    
    public async Task RendezvouzRequestAsync(Guid doctorId, Guid patientId, DateTime dateTime, string? description){
        await patientRepository.RendezvouzRequestAsync(doctorId, patientId, dateTime, description);
    }
        
}
