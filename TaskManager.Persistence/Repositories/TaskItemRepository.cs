using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Persistence.Repositories
{
    public class TaskItemRepository : IRepository<TaskItem>
    {
        private readonly AppDbContext context;

        public TaskItemRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await context.TaskItems.FindAsync(id);
        }

        public async Task<TaskItem> AddAsync(TaskItem entity)
        {
            context.TaskItems.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TaskItem> UpdateAsync(TaskItem entity)
        {
            context.TaskItems.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TaskItem entity)
        {
            context.TaskItems.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
