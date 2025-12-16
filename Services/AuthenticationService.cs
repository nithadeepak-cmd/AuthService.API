using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.API.DTOs;
using AuthService.API.Interfaces;
using AuthService.API.Models;
using Microsoft.IdentityModel.Tokens;
using AuthService.API.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AuthService.API.Services
{
    public class AuthenticationService : IAuthService
    {
        // private readonly List<User> _users = new(); // In-memory user store
        private readonly IConfiguration _config;
        private readonly AuthDbContext _context;

        public AuthenticationService(AuthDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            //Write full register logic here


            // 1. Check if user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower()
            == request.Email.ToLower());
            if (existingUser != null)
                throw new Exception("User already exists");

            // 2. Hash the password - BCrypt
            string passwordHash =BCrypt.Net.BCrypt.HashPassword(request.Password);

            //3. create user object
            var user = new User
            {

                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            //4. save user into database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 5. return response DTO
            return new AuthResponseDTO()
            {
                Token = string.Empty,
                RefreshToken = string.Empty,
                Username = user.Username,
                Role = user.Role
            };


        }
        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            //write full login logic here

            //1. check if user exists and active
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
               throw new Exception("Invalid email or password");
            if(!user.IsActive)
                throw new Exception("User account is inactive");

            //2. Bcrypt password verification
            bool ispasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            // 3. Validate password
            if (!ispasswordValid)
                throw new Exception("Invalid email or password");

            //4. Generate JWT token
            string token = GenerateJwtToken(user.Username, user.Role);

            //5. Return AuthResponseDTO
            return new AuthResponseDTO
            {
                Token = token,
                RefreshToken = string.Empty,
                Username = user.Username,
                Role = user.Role
            };
        }
        public string GenerateJwtToken(string username, string role)
        {
            //write jwt logic here
            var jwtSettings = _config.GetSection("JwtSettings");
            //var key = _config.GetValue<string>("JwtSettings:Key");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)

            };
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
