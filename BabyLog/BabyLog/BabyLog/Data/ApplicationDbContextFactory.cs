using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BabyLog.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Factory used by Entity Framework Core at design time
        // Used when running migrations
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // SQL Server connection used for migrations
            optionsBuilder.UseSqlServer(
                "Server=tcp:babyserver.database.windows.net,1433;Initial Catalog=BabyFællesskabDatabase;Persist Security Info=False;User ID=BabyFællesskabServer;Password=BabyFællesskab26;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}