using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Application.DTOs
{
    public class TaskListDto
    {
        public Guid TaskListId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
    public class CreateTaskListDto
    {
        public string Name { get; set; } = null!;
    }
    public class UpdateTaskListDto
    {
        public Guid TaskListId { get; set; }
        public string Name { get; set; } = null!;
    }

    public class TaskListDto_v2
    {
        public Guid TaskListId;
        public Guid UserId;
        public string Title { get; set; } = null!;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public DateTime? CalendarDay;
    }
}
