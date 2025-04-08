using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Common;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dtos;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskItemService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TaskItemDto>>> GetAllAsync()
        {
            var taskItems = await _context.TaskItems.ToListAsync();

            return taskItems is not null
                ? Result<IEnumerable<TaskItemDto>>
                    .Ok(_mapper.Map<List<TaskItemDto>>(taskItems))
                : Result<IEnumerable<TaskItemDto>>.Fail("Task items not found");
        }

        public async Task<Result<TaskItemDto>> GetByIdAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            return taskItem is not null
                ? Result<TaskItemDto>.Ok(_mapper.Map<TaskItemDto>(taskItem))
                : Result<TaskItemDto>.Fail("Task item not found");
        }

        public async Task<Result<TaskItemDto>> CreateAsync(TaskItemDto model)
        {
            var taskItem = _mapper.Map<TaskItem>(model);

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return Result<TaskItemDto>.Ok(_mapper.Map<TaskItemDto>(taskItem));
        }

        public async Task<Result<TaskItemDto>> UpdateAsync(int id, TaskItemDto model)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
                return Result<TaskItemDto>.Fail("Task Item not found.");

            _mapper.Map(model, taskItem);

            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();

            return Result<TaskItemDto>.Ok(_mapper.Map<TaskItemDto>(taskItem));
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
                return Result<string>.Fail("Task Item not found.");

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return Result<string>.Ok("Task Item is deleted");
        }
    }
}
