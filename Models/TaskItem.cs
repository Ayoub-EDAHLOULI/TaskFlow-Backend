using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public bool IsImportant { get; set; }
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int UserId { get; set; }
        public User? User { get; set; } // Make User property nullable
    }
}
