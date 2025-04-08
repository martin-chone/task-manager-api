using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Common;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            return users is not null
                ? Result<IEnumerable<UserDto>>
                    .Ok(_mapper.Map<IEnumerable<UserDto>>(users))
                : Result<IEnumerable<UserDto>>.Fail("Users not found");
        }

        public async Task<Result<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user is not null
                ? Result<UserDto>.Ok(_mapper.Map<UserDto>(user))
                : Result<UserDto>.Fail("User not found");
        }   
    }
}
