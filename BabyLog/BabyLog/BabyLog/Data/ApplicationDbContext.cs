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

        // Children table
        public DbSet<Child> Children { get; set; }

        // Sleeps table
        public DbSet<Sleep> Sleeps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Child is mapped to table, so EF Core can create it with migration
            modelBuilder.Entity<Child>()
                .ToTable("Children")
                .HasKey(c => c.ChildId);

            // Sleep is mapped to table, so EF Core can create it with migration
            modelBuilder.Entity<Sleep>()
                .ToTable("Sleeps")
                .HasKey(s => s.SleepId);
        }
    }
}