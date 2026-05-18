using BabyLog.Models;

namespace BabyLog.Repositories
{
    public interface ISleepRepository : IRepository<Sleep>
    {
        // Gets all sleep registrations for one child
        Task<List<Sleep>> GetSleepByChildIdAsync(int childId);

        // Creates sleep using stored procedure
        Task CreateSleepAsync(Sleep sleep);

        // Updates sleep using stored procedure
        Task UpdateSleepAsync(Sleep sleep);

        // Deletes sleep using stored procedure
        Task DeleteSleepAsync(int sleepId);
    }
}