using AutoMapper;
using TaskManager.Application.Dtos;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Shared;

namespace TaskManager.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IRepository<TaskItem> repository;
        private readonly IMapper mapper;

        public TaskItemService(IRepository<TaskItem> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<TaskItemDto>>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();

            return entities is not null
                ? Result<IEnumerable<TaskItemDto>>
                    .Ok(mapper.Map<IEnumerable<TaskItemDto>>(entities))
                : Result<IEnumerable<TaskItemDto>>.Fail("Task items not found");
        }

        public async Task<Result<TaskItemDto>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            return entity is not null
                ? Result<TaskItemDto>.Ok(mapper.Map<TaskItemDto>(entity))
                : Result<TaskItemDto>.Fail("Task item not found");
        }

        public async Task<Result<TaskItemDto>> CreateAsync(TaskItemDto dto)
        {
            var entity = mapper.Map<TaskItem>(dto);
            var result =  await repository.AddAsync(entity);

            return Result<TaskItemDto>.Ok(mapper.Map<TaskItemDto>(result));
        }

        public async Task<Result<TaskItemDto>> UpdateAsync(int id, TaskItemDto dto)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return Result<TaskItemDto>.Fail("Task Item not found.");

            mapper.Map(dto, entity);
            var result = await repository.UpdateAsync(entity);

            return Result<TaskItemDto>.Ok(mapper.Map<TaskItemDto>(result));
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return Result<string>.Fail("Task Item not found.");

            await repository.DeleteAsync(entity);

            return Result<string>.Ok("Task Item is deleted");
        }
    }
}
