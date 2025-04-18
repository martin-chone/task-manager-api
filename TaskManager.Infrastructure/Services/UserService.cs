using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Dtos;
using TaskManager.Application.Interfaces;
using TaskManager.Shared;

namespace TaskManager.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDto>>> GetUsersInRoleAsync(string roleName)
        {
            var users = await userManager.GetUsersInRoleAsync(roleName);

            return users is not null
                ? Result<IEnumerable<UserDto>>
                    .Ok(mapper.Map<IEnumerable<UserDto>>(users))
                : Result<IEnumerable<UserDto>>.Fail("Users not found");
        }

        public async Task<Result<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user is not null
                ? Result<UserDto>.Ok(mapper.Map<UserDto>(user))
                : Result<UserDto>.Fail("User not found");
        }   
    }
}
