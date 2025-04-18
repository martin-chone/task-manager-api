using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Dtos;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<UserDto, IdentityUser>().ReverseMap();
        }
    }
}
