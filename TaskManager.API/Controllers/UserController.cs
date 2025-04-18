using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Extensions;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var result = await userService.GetUsersInRoleAsync(roleName);

            return result.ToActionResult();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserByName(string email)
        {
            var result = await userService.GetUserByEmailAsync(email);

            return result.ToActionResult();
        }
    }
}
