using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Extensions;
using TaskManager.Application.Dtos;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService taskItemService;
        private readonly ILogger<TaskItemController> logger;

        public TaskItemController(ITaskItemService taskItemService, ILogger<TaskItemController> logger)
        {
            this.taskItemService = taskItemService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await taskItemService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await taskItemService.GetByIdAsync(id);

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemDto dto)
        {
            var result = await taskItemService.CreateAsync(dto);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItemDto dto)
        {
            var result = await taskItemService.UpdateAsync(id, dto);

            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await taskItemService.DeleteAsync(id);

            return result.ToActionResult();
        }
    }
}
