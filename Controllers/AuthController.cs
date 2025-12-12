using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthService.API.DTOs;
using AuthService.API.Interfaces;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //POST:api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //POST:api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message=ex.Message});
            }

        }


    }

}
