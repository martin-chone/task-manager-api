using TaskManager.Application.Dtos;
using TaskManager.Shared;

namespace TaskManager.Application.Interfaces
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName);
        public Task<Result<UserDto>> GetUserByEmailAsync(string email);

    }
}
