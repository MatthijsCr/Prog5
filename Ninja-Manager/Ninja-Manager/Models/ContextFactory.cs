using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Ninja_Manager.Models;

namespace BumboApp.Models
{
    public class ContextFactory : IDesignTimeDbContextFactory<NinjaDbContext>
    {
        public ContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

        public NinjaDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<NinjaDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("NinjaDb"));

            return new NinjaDbContext(builder.Options);
        }
    }
}
