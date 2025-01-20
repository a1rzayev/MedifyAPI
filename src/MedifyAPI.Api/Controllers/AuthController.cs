using Microsoft.AspNetCore.Mvc;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Core.Models.Base;
using MedifyAPI.Core.DTO;
using MedifyAPI.Core.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
using MedifyAPI.Infrastructure.Repositories.EfCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedifyAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly MedifyDbContext _context;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public AuthController(
                              ITokenService tokenService,
                              MedifyDbContext context,
                              IDoctorService doctorService,
                              IPatientService patientService)
        {
            _tokenService = tokenService;
            _context = context;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupDto signupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            var existingUser = await _patientService.GetByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            // Create Patient
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Email = signupDto.Email,
                Name = signupDto.Name,
                Surname = signupDto.Surname,
                DateJoined = DateTime.UtcNow
            };

            // Create the user with the provided password
            _patientService.AddAsync(patient);


            return Ok(new { message = "Patient registered successfully." });
        }

        // Login Method
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _patientService.GetByEmailAsync(loginDto.Email);
            if (user == null || !( user.Password == loginDto.Password))
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT tokens
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Save refresh token in the database
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

        // Logout Method
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken != null)
            {
                var tokenEntity = _context.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
                if (tokenEntity != null)
                {
                    tokenEntity.IsRevoked = true;
                    _context.RefreshTokens.Update(tokenEntity);
                    await _context.SaveChangesAsync();
                }
            }

            return Ok("User logged out successfully.");
        }
    }
}
