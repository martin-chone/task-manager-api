using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TaskManagerAPI.Dtos;
using TaskManagerAPI.Extensions;
using TaskManagerAPI.Services.Auth;

namespace TaskManagerAPI.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model, string roleName)
        {
            var result = await _authService.RegisterAsync(model, roleName);

            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            var result = await _authService.LoginAsync(model);
            
            return result.ToActionResult();
        }

        [HttpGet]
        public IActionResult GetJWTKey()
        {
            byte[] key = new byte[32]; // 32 bytes = 256 bits (pour HS256)
            RandomNumberGenerator.Fill(key);

            return Ok(Convert.ToBase64String(key));
        }
    }
}
