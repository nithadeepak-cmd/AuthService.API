using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthService.API.DTOs;
using AuthService.API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")] //Sets the base route for this controller to api/auth
    [ApiController]  //Indicates that this class is an API controller - provides automatic model validation and other features
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

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        { 
            var users=await  _authService.GetAllUsersAsync();
            return Ok(users);
        }
        /*
         * Commenting test end points used for jwt verification
         * ------------------------------------------------------
         * 
                [Authorize(Roles = "Admin")]  //only users with Admin role can access
                [HttpGet("admin-only")]
                public IActionResult AdminOnly()
                {
                    {     return Ok(new { Message = "You are an Admin.Only Admins can access this endpoint" , user=User.Identity?.Name}); }
                }

                [Authorize]//protects this endpoint - only authenticated users can access - without a valid token will get 401 Unauthorized
                [HttpGet("user-profile")]
                public IActionResult UserProfile()
                {
                    {     return Ok(new { Message = "This is a user profile endpoint. Any authenticated user can access this.", 
                        user=User.Identity?.Name,Role=User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value}); }
                }

        */
    }

}
