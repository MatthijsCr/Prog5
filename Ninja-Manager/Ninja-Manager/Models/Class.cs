using Microsoft.EntityFrameworkCore;

namespace Ninja_Manager.Models
{
    public class NinjaDbContext : DbContext
    {
        public NinjaDbContext()
        {
        }

        public NinjaDbContext(DbContextOptions<NinjaDbContext> options)
            : base(options)
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Ninja"));
    }
}
