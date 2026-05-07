using BabyLog.Models;

namespace BabyLog.Repositories
{
    public interface IChildRepository : IRepository<Child>
    {
        // Gets all children for one customer
        Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId);
    }
}