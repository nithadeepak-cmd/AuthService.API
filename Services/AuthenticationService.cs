using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.API.DTOs;
using AuthService.API.Interfaces;
using AuthService.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.API.Services
{
    public class AuthenticationService:IAuthService
    {
        private readonly List<User> _users = new();
        private readonly IConfiguration _config;
        public AuthenticationService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            //Write full register logic here

            // 1. Check if user already exists
            if (_users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                throw new Exception("User already exists");

            // 2. Hash the password
            string passwordHash =Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            //3. create user object
            var user = new User
            {
                Id = _users.Count + 1,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role
            };

            //4.save user into the memory list
            _users.Add(user);

            // 5.Generate JWTToken for this user
            string token = GenerateJwtToken(user.Username, user.Role);

            // 6. return response DTO
            return new AuthResponseDTO()
            {
                Token = token,
                RefreshToken = "",
                Username=user.Username,
                Role=user.Role
            };


        }
        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            //write full login logic here

            //1. check if user exists
            var user=_users.FirstOrDefault(u=>u.Email.ToLower() == request.Email.ToLower());
            if (user == null)
                throw new Exception("Invalid email or password");

            //2. Hash incoming password - same method as Register
            string incomingPasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            // 3. compare hash with stored hash
            if(incomingPasswordHash != user.PasswordHash)
                throw new Exception("Invalid email or password");

            //4. Generate JWT token
            string token = GenerateJwtToken(user.Username, user.Role);

            //5. Return AuthResponseDTO
            return new AuthResponseDTO
            {
                Token = token,
                RefreshToken = "",
                Username=user.Username,
                Role=user.Role
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
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
