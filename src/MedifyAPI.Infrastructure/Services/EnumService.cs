using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Repositories;
using System.Data.Common;
using MedifyAPI.Core.Models.Requests;
using MedifyAPI.Core.Enums;

namespace MedifyAPI.Infrastructure.Services;


public class EnumService : IEnumService
{
    public Task<string[]> GetAllGenderEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(GenderEnum)));
    }

    public Task<string[]> GetAllHealthEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(HealthEnum)));
    }

    public Task<string[]> GetAllHospitalTypeEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(HospitalTypeEnum)));
    }

    public Task<string[]> GetAllRequestStateEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(RequestStateEnum)));
    }

    public Task<string[]> GetAllRoleEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(RoleEnum)));
    }

    public Task<string[]> GetAllSpecialityEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(SpecialityEnum)));
    }

    public Task<string[]> GetAllWeekDayEnum()
    {
        return Task.FromResult(Enum.GetNames(typeof(WeekDayEnum)));
    }
}
