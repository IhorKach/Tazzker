using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazzker.Domain
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

    }
}
