using TaskManagerAPI.Common;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services
{
    public interface IUserService
    {
        public Task<Result<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName);
        public Task<Result<UserDto>> GetUserByNameAsync(string userName);

    }
}
