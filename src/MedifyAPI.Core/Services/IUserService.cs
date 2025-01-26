using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Base;

namespace MedifyAPI.Core.Services;

public interface IUserService
{
    Task AddAsync(IUser user, string Role);
    Task<IUser?> GetByEmailAsync(string email);
    
    Task SetValidation(Guid id, bool value, string Role);
}