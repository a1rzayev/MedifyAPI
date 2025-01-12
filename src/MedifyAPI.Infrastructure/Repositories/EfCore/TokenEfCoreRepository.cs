using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MedifyAPI.Infrastructure.Repositories.EfCore;


public class TokenEfCoreRepository : ITokenRepository
{
    private readonly MedifyDbContext _context;
    private readonly IConfiguration _configuration;

    public TokenEfCoreRepository(MedifyDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:AccessTokenLifetimeMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
    public async Task StoreRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var expirationDate = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:RefreshTokenLifetimeMinutes"]));
        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            ExpirationDate = expirationDate
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();
    }

    // Validate the refresh token
    public async Task<RefreshToken> ValidateRefreshTokenAsync(string refreshToken)
    {
        var storedRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedRefreshToken == null || storedRefreshToken.IsRevoked)
        {
            throw new SecurityTokenException("Invalid or revoked refresh token.");
        }

        if (storedRefreshToken.ExpirationDate < DateTime.UtcNow)
        {
            throw new SecurityTokenException("Refresh token has expired.");
        }

        return storedRefreshToken;
    }

    // Revoke a refresh token (e.g., during logout)
    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var storedRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedRefreshToken != null)
        {
            storedRefreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }
}
