using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Tazzker.Infrastructure.Contexts
{
    public class TazzkerDbContextFactory : IDesignTimeDbContextFactory<TazzkerDbContext>
    {
        public TazzkerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TazzkerAPI"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TazzkerDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

            return new TazzkerDbContext(optionsBuilder.Options);

        }

    }
}
