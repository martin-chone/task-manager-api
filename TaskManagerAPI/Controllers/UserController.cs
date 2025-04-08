using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Extensions;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var result = await _userService.GetUsersInRoleAsync(roleName);

            return result.ToActionResult();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var result = await _userService.GetUserByNameAsync(userName);

            return result.ToActionResult();
        }
    }
}
