using BabyLog.Data;
using BabyLog.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyLog.Repositories
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        // Gets all children connected to a specific customer
        public async Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId)
        {
            return await _context.Children
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }
    }
}