using BabyLog.Models;

namespace BabyLog.Repositories
{
    public interface IChildRepository : IRepository<Child>
    {
        // Gets all children for one customer
        Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId);

        // Creates child using stored procedure
        Task CreateChildAsync(Child child);

        // Updates child using stored procedure
        Task UpdateChildAsync(Child child);

        // Deletes child using stored procedure
        Task DeleteChildAsync(int childId);
    }
}