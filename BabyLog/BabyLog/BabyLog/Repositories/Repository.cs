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

        // Table or view reference
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            // Gets DbSet from DbContext
            _dbSet = context.Set<T>();
        }

        // Gets all records
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Gets one record by Id
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Adds a new record
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Updates a record
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        // Deletes a record
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Saves all changes to database
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}