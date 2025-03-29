using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Domain;

namespace Tazzker.Infrastructure
{
    public class TazzkerDbContext : DbContext
    {
        public TazzkerDbContext(DbContextOptions<TazzkerDbContext> options) : base(options) { }
        DbSet<TaskItem> TaskItems { get; set; }
        DbSet<TaskList> TaskLists { get; set; }
        DbSet<User> Users { get; set; }

    }
}
