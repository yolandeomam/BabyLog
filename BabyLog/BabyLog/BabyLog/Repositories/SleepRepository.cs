using BabyLog.Data;
using BabyLog.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BabyLog.Repositories
{
    public class SleepRepository : Repository<Sleep>, ISleepRepository
    {
        public SleepRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        // Gets all sleep registrations for one child from SQL view
        public async Task<List<Sleep>> GetSleepByChildIdAsync(int childId)
        {
            return await _context.Sleeps
                .Where(s => s.ChildId == childId)
                .ToListAsync();
        }

        // Gets one sleep registration by SleepId from SQL view
        public override async Task<Sleep?> GetByIdAsync(int id)
        {
            return await _context.Sleeps
                .FirstOrDefaultAsync(s => s.SleepId == id);
        }

        // Creates sleep using stored procedure
        public async Task CreateSleepAsync(Sleep sleep)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateSleep @ChildId, @SleepDate, @StartTime, @EndTime",
                new SqlParameter("@ChildId", sleep.ChildId),
                new SqlParameter("@SleepDate", sleep.SleepDate.Date),
                new SqlParameter("@StartTime", sleep.StartTime),
                new SqlParameter("@EndTime", sleep.EndTime)
            );
        }

        // Updates sleep using stored procedure
        public async Task UpdateSleepAsync(Sleep sleep)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateSleep @SleepId, @SleepDate, @StartTime, @EndTime",
                new SqlParameter("@SleepId", sleep.SleepId),
                new SqlParameter("@SleepDate", sleep.SleepDate.Date),
                new SqlParameter("@StartTime", sleep.StartTime),
                new SqlParameter("@EndTime", sleep.EndTime)
            );
        }

        // Deletes sleep using stored procedure
        public async Task DeleteSleepAsync(int sleepId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteSleep @SleepId",
                new SqlParameter("@SleepId", sleepId)
            );
        }
    }
}