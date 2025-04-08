using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Common;
using TaskManagerAPI.Dtos;
using TaskManagerAPI.Factories.Claims;

namespace TaskManagerAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IClaimFactory _claimFactory;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IClaimFactory claimFactory, 
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimFactory = claimFactory;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<Result<string>> RegisterAsync(RegisterUserDto model, string roleName)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if(userExists != null)
                return Result<string>.Fail($"{model.UserName} already exist");

            var user = _mapper.Map<IdentityUser>(model);
            var userResult = await _userManager.CreateAsync(user, model.Password);

            if (!userResult.Succeeded)
                return Result<string>.Fail($"Failed to create user");
                
            if(!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                    return Result<string>.Fail("Failed to create role");
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                var roleAssignment = await _userManager.AddToRoleAsync(user, roleName);
                if (!roleAssignment.Succeeded)
                    return Result<string>.Fail("Failed to assign role");
            }

            return Result<string>.Ok("User registered successfully");
        }

        public async Task<Result<AuthResultDto>> LoginAsync(LoginUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Result<AuthResultDto>.Fail("Invalid username or password");

            var claims = await _claimFactory.GetClaimsForUserAsync(user);
            var result = _tokenService.GenerateToken(claims);

            var response = new AuthResultDto
            {
                Token = result.Token,
                ExpiresIn = (int)(result.ExpiresAt - DateTime.UtcNow).TotalSeconds,
                UserName = user.UserName!
            };

            return Result<AuthResultDto>.Ok(response);
        }
    }
}
