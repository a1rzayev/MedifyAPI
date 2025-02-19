using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Services;

public interface IEnumService
{
    Task<string[]> GetAllGenderEnum();
    Task<string[]> GetAllHealthEnum();
    Task<string[]> GetAllHospitalTypeEnum();
    Task<string[]> GetAllRequestStateEnum();
    Task<string[]> GetAllRoleEnum();
    Task<string[]> GetAllSpecialityEnum();
    Task<string[]> GetAllWeekDayEnum();

}