using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Application.DTOs
{
    public class CreateTaskItemDto
    {
        public Guid ListId { get; set; }
        public Guid? ParentTaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
        public float Order { get; set; }
    }
    public class TaskItemDto
    {
        public Guid TaskId { get; set; }
        public Guid ListId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? ParentTaskId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public float Order { get; set; }
    }

    public class UpdateTaskItemDto
    {
        public Guid TaskId { get; set; }
        public Guid? ListId { get; set; }
        public Guid? ParentTaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ReminderAt { get; set; }
        public DateTime? DueTime { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsDeleted { get; set; }
        public float? Order { get; set; }
    }
    public class TaskItemFilterDto
    {
        public Guid? ListId { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? DueTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;
        public float Order { get; set; }
    }

    public class TaskItemDto_v2
    {
        public Guid TaskId;
        public Guid UserId;
        public Guid SubListId;
        public float Order;

        public string Title = null!;
        public string Description = null!;

        public DateTime UpdatedAt;
        public DateTime? ReminderAt;
        public DateTime? DueTime;

        public bool IsCompleted;
        public bool IsDeleted;
    }
}