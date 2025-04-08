using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManagerAPI.Dtos;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, RegisterUserDto>().ReverseMap();
            CreateMap<IdentityUser, LoginUserDto>().ReverseMap();
            CreateMap<IdentityUser, UserDto>().ReverseMap();

            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        }
    }
}
