using Microsoft.EntityFrameworkCore;
using activities.api.Models;

namespace activities.api.Data
{
    public class ActivitiesDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ActivitiesDbContext(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        // Add DbSet properties for your entities here

        public DbSet<Activity> Activities { get; set; }
    }
}