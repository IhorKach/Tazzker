using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

    }
}
