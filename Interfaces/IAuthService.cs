

using AuthService.API.DTOs;

namespace AuthService.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request);
        Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
        string GenerateJwtToken(string username, string role);
    }
}
