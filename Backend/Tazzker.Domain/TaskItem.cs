using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Domain
{
    public class TaskItem
    {
        [Key]
        public Guid TaskId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid ListId { get; set; }
        public Guid? ParentTaskId { get; set; }
        public float Order {  get; set; } = 0f;

        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }

        public bool IsCompleted { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
