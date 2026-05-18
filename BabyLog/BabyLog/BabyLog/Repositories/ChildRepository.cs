using BabyLog.Data;
using BabyLog.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BabyLog.Repositories
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        // Gets all children connected to a specific customer from SQL view
        public async Task<List<Child>> GetChildrenByCustomerIdAsync(int customerId)
        {
            return await _context.Children
                .Where(c => c.CustomerId == customerId)
                .ToListAsync();
        }

        // Gets one child by id from SQL view
        public override async Task<Child?> GetByIdAsync(int id)
        {
            return await _context.Children
                .FirstOrDefaultAsync(c => c.ChildId == id);
        }

        // Creates child using stored procedure
        public async Task CreateChildAsync(Child child)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CreateChild @CustomerId, @FirstName, @BirthDate, @Gender, @ChildCreatedDate",
                new SqlParameter("@CustomerId", child.CustomerId),
                new SqlParameter("@FirstName", child.FirstName),
                new SqlParameter("@BirthDate", child.BirthDate),
                new SqlParameter("@Gender", (int)child.Gender),
                new SqlParameter("@ChildCreatedDate", child.ChildCreatedDate)
            );
        }

        // Updates child using stored procedure
        public async Task UpdateChildAsync(Child child)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateChild @ChildId, @FirstName, @BirthDate, @Gender",
                new SqlParameter("@ChildId", child.ChildId),
                new SqlParameter("@FirstName", child.FirstName),
                new SqlParameter("@BirthDate", child.BirthDate),
                new SqlParameter("@Gender", (int)child.Gender)
            );
        }

        // Deletes child using stored procedure
        public async Task DeleteChildAsync(int childId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteChild @ChildId",
                new SqlParameter("@ChildId", childId)
            );
        }
    }
}