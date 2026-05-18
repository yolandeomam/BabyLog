using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BabyLog.Data
{
    // Factory used by Entity Framework Core at design time
    // Used when running migrations
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Creates DbContext manually for EF Core tools
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // SQL Server connection used for migrations
            optionsBuilder.UseSqlServer(
                "Server=tcp:babyserver.database.windows.net,1433;Initial Catalog=BabyFællesskabDatabase;Persist Security Info=False;User ID=BabyFællesskabServer;Password=BabyFællesskab26;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            // Returns configured DbContext
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}