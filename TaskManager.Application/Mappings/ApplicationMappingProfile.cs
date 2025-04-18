using AutoMapper;
using TaskManager.Application.Dtos;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<RegisterRequest, LoginRequest>().ReverseMap();

            CreateMap<TaskItemDto, TaskItem>().ReverseMap();
        }
    }
}
