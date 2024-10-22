using Microsoft.EntityFrameworkCore;

namespace Ninja_Manager.Models
{
    public class NinjaManagerDbContext : DbContext
    {
        public NinjaManagerDbContext(DbContextOptions<NinjaManagerDbContext> options) : base(options) { }
    }
}
