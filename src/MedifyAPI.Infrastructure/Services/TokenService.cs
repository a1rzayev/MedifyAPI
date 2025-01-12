using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Core.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MedifyAPI.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository tokenRepository;

    public TokenService(ITokenRepository tokenRepository)
    {
        this.tokenRepository = tokenRepository;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        return this.tokenRepository.GenerateAccessToken(claims);
    }

    public string GenerateRefreshToken()
    {
        return this.tokenRepository.GenerateRefreshToken();
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        return this.tokenRepository.GetPrincipalFromExpiredToken(token);
    }
    public async Task StoreRefreshTokenAsync(Guid userId, string refreshToken)
    {
        await this.tokenRepository.StoreRefreshTokenAsync(userId, refreshToken);
    }

    // Validate the refresh token
    public async Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken)
    {
        return await this.tokenRepository.ValidateRefreshTokenAsync(refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        await this.tokenRepository.RevokeRefreshTokenAsync(refreshToken);
    }

}
