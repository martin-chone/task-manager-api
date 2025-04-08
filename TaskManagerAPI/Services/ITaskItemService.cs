using TaskManagerAPI.Common;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services
{
    public interface ITaskItemService
    {
        Task<Result<IEnumerable<TaskItemDto>>> GetAllAsync();
        Task<Result<TaskItemDto>> GetByIdAsync(int id);
        Task<Result<TaskItemDto>> CreateAsync(TaskItemDto model);
        Task<Result<TaskItemDto>> UpdateAsync(int id, TaskItemDto model);
        Task<Result<string>> DeleteAsync(int id);
    }
}
