using BabyLog.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyLog.Repositories
{
    // Generic repository implementation
    // Reused by ChildRepository and future repositories
    public class Repository<T> : IRepository<T> where T : class
    {
        // Database context
        protected readonly ApplicationDbContext _context;

        // Table reference
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            // Gets table from DbContext
            _dbSet = context.Set<T>();
        }

        // Gets all records
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Gets one record by Id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Adds a new record
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Updates a record
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Deletes a record
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Saves all changes to database
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}