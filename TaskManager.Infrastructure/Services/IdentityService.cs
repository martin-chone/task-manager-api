using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Dtos.Claims;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Factories.Claims;
using TaskManager.Shared;

namespace TaskManager.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IClaimsFactory claimsFactory;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public IdentityService(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IClaimsFactory claimsFactory, 
            ITokenService tokenService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.claimsFactory = claimsFactory;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        public async Task<Result> RegisterAsync(RegisterRequest request, string roleName)
        {
            var userExists = await userManager.FindByEmailAsync(request.Email);

            if(userExists != null)
                return Result.Fail($"{request.Email} already exist");

            var user = mapper.Map<IdentityUser>(request);
            var userResult = await userManager.CreateAsync(user, request.Password);

            if (!userResult.Succeeded)
                return Result.Fail($"Failed to create user");
                
            if(!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                    return Result.Fail("Failed to create role");
            }

            if (await roleManager.RoleExistsAsync(roleName))
            {
                var roleAssignment = await userManager.AddToRoleAsync(user, roleName);
                if (!roleAssignment.Succeeded)
                    return Result.Fail("Failed to assign role");
            }

            return Result.Ok();
        }

        public async Task<Result<AuthResponse>> RegisterAndLoginAsync(RegisterRequest request, string roleName)
        {
            var registrationResult = await RegisterAsync(request, roleName);

            if (registrationResult.IsFailure)
                return Result<AuthResponse>.Fail(registrationResult.Error);

            var loginRequest = mapper.Map<LoginRequest>(request);
            return await LoginAsync(loginRequest);
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
                return Result<AuthResponse>.Fail("Invalid username or password");

            var claimsRequest = new ClaimsRequest()
            {
                Id = user.Id,
                Email = user.Email,
                Roles = await userManager.GetRolesAsync(user)
            };

            var claims = claimsFactory.GetClaimsForUser(claimsRequest);
            var result = tokenService.GenerateToken(claims);

            var response = new AuthResponse
            {
                Token = result.Token,
                ExpiresAt = result.ExpiresAt
            };

            return Result<AuthResponse>.Ok(response);
        }
    }
}
