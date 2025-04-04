using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Domain;

namespace Tazzker.Infrastructure.Contexts
{
    public class TazzkerDbContext : DbContext
    {
        public TazzkerDbContext(DbContextOptions<TazzkerDbContext> options) : base(options) { }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
