using Microsoft.AspNetCore.Mvc;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Repositories;
using MedifyAPI.Core.Models.Base;
using BCrypt.Net;
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
            var existingUser = await _patientService.GetByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email is already in use." });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(signupDto.Password);

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Email = signupDto.Email,
                Name = signupDto.Name,
                Surname = signupDto.Surname,
                Password = hashedPassword,
                DateJoined = DateTime.UtcNow
            };

            await _patientService.AddAsync(patient);
            await _patientService.SetValidation(patient.Id, false);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, patient.Id.ToString()),
                new Claim(ClaimTypes.Email, patient.Email),
            };

            var token = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.StoreRefreshTokenAsync(patient.Id, refreshToken);

            return Ok(new
            {
                message = "Patient registered successfully.",
                accessToken = token,
                refreshToken = refreshToken
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }
            else if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Find user
            var user = await _patientService.GetByEmailAsync(loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.StoreRefreshTokenAsync(user.Id, refreshToken);

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }


        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto refreshTokenDto)
        {
            await _tokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
            return Ok("Logged out successfully.");
        }
    }
}
