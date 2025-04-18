using TaskManager.Application.Dtos;
using TaskManager.Shared;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<Result<IEnumerable<TaskItemDto>>> GetAllAsync();
        Task<Result<TaskItemDto>> GetByIdAsync(int id);
        Task<Result<TaskItemDto>> CreateAsync(TaskItemDto dto);
        Task<Result<TaskItemDto>> UpdateAsync(int id, TaskItemDto dto);
        Task<Result<string>> DeleteAsync(int id);
    }
}
