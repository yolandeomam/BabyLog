namespace BabyLog.Repositories
{
    // Generic repository interface
    // Used for reusable CRUD operations
    public interface IRepository<T> where T : class
    {
        // Gets all entities
        Task<List<T>> GetAllAsync();

        // Gets one entity by Id
        Task<T?> GetByIdAsync(int id);

        // Creates a new entity
        Task AddAsync(T entity);

        // Updates an entity
        void Update(T entity);

        // Deletes an entity
        void Delete(T entity);

        // Saves changes to database
        Task SaveAsync();
    }
}