using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Domain
{
    public class TaskList
    {
        [Key]
        public int TaskListId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
