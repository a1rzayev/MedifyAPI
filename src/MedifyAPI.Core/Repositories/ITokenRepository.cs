using System.Security.Claims;
using MedifyAPI.Core.Models;

namespace MedifyAPI.Core.Repositories;
public interface ITokenRepository
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    Task StoreRefreshTokenAsync(Guid userId, string refreshToken);
    Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken);
}