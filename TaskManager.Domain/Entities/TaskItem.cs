using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}