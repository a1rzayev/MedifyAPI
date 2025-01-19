using Microsoft.AspNetCore.Mvc;
using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Base;
using MedifyAPI.Core.DTO;
using MedifyAPI.Core.Services;
using MedifyAPI.Infrastructure.Repositories.EfCore.DbContexts;
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
        private readonly UserManager<IPerson> _userManager;
        private readonly SignInManager<IPerson> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly MedifyDbContext _context;

        public AuthController(UserManager<IPerson> userManager,
                              SignInManager<IPerson> signInManager,
                              ITokenService tokenService,
                              MedifyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
        }

        // Sign Up Method
        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupDto signupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(signupDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            IPerson person;

            if (signupDto.Role == "Doctor")
            {
                var doctor = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Email = signupDto.Email,
                    Name = signupDto.Name,
                    Surname = signupDto.Surname,
                    DateJoined = DateTime.UtcNow
                };
                person = doctor;
            }
            else if (signupDto.Role == "Patient")
            {
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    Email = signupDto.Email,
                    Name = signupDto.Name,
                    Surname = signupDto.Surname,
                    DateJoined = DateTime.UtcNow
                };
                person = patient;
            }
            else
            {
                return BadRequest("Invalid user type.");
            }

            var result = await _userManager.CreateAsync(person, signupDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Optionally, you can assign roles here
            await _userManager.AddToRoleAsync(person, signupDto.Role);

            return Ok(new { message = "User registered successfully." });
        }

        // Login Method
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
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
