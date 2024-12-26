using Microsoft.EntityFrameworkCore;

namespace UserJwtSolution.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> As_Users { get; set; }
    }
}
