using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Ninja_Manager.Models;

namespace Ninja_Manager.Models
{
    public class ContextFactory : IDesignTimeDbContextFactory<NinjaManagerDbContext>
    {
        public ContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

        public NinjaManagerDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<NinjaManagerDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("NinjaDb"));

            return new NinjaManagerDbContext(builder.Options);
        }
    }
}
