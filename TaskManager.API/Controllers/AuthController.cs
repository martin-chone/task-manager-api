using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TaskManager.API.Extensions;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Interfaces.Auth;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, string roleName)
        {
            var result = await authService.RegisterAsync(request, roleName);

            return result.ToActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await authService.LoginAsync(request);
            
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
