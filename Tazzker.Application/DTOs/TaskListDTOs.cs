using System;
using System.Collections.Generic;
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
}
