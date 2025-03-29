using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Domain
{
    public class TaskList
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
