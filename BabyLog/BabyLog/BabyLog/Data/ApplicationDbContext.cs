using Microsoft.EntityFrameworkCore;
using BabyLog.Models;

namespace BabyLog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Children table in database
        public DbSet<Child> Children { get; set; }
    }
}