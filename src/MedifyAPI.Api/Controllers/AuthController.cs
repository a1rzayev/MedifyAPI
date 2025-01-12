using Microsoft.AspNetCore.Mvc;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Base;
using MedifyAPI.Core.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using MedifyAPI.Core.DTO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;

namespace MedifyAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IPerson> _userManager; 
    private readonly ITokenService _tokenService;
    private readonly MedifyDbContext _context;

    public AuthController(UserManager<IPerson> userManager, ITokenService tokenService, MedifyDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized("Invalid credentials");
        }

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {

        var refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken != null)
        {
            var tokenEntity = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
            if (tokenEntity != null)
            {
                tokenEntity.IsRevoked = true;
                _context.RefreshTokens.Update(tokenEntity);
                _context.SaveChanges();
            }
        }

        return Ok("User logged out successfully");
    }
}