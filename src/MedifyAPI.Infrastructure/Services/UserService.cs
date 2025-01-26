using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Infrastructure.Services;


public class UserService : IUserService
{
    private readonly IPatientService patientService;
    private readonly IDoctorService doctorService;

    public UserService(IPatientService patientService, IDoctorService doctorService)
    {
        this.patientService = patientService;
        this.doctorService = doctorService;
    }
    public async Task AddAsync(IUser user, string Role)
    {
        if(Role == "patient"){ 
            await patientService.AddAsync(new Patient{
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                DateJoined = user.DateJoined
            });
        }
        else if(Role == "doctor"){ 
            await doctorService.AddAsync(new Doctor{
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                DateJoined = user.DateJoined
            });
        }
    }
    public async Task<IUser?> GetByEmailAsync(string email){
        
        var patient = await patientService.GetByEmailAsync(email);
        var doctor = await doctorService.GetByEmailAsync(email);
        if(doctor != null ) return new IUser{
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Email = doctor.Email,
                Password = doctor.Password,
                DateJoined = doctor.DateJoined
            };
        else if(patient != null ) return new IUser{
                Id = patient.Id,
                Name = patient.Name,
                Surname = patient.Surname,
                Email = patient.Email,
                Password = patient.Password,
                DateJoined = patient.DateJoined
            };
        else return null;
    }


    
    public async Task SetValidation(Guid id, bool value, string Role){
        if(Role == "patient") await patientService.SetValidation(id, value);
        else if(Role == "doctor") await doctorService.SetValidation(id, value);
    }

}
