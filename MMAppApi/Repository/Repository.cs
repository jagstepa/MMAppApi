using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MMAppApi.Helpers;
using MMAppApi.Interfaces;
using MMAppApi.Models;
using System.Linq.Expressions;

namespace MMAppApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MmappContext _context;

        private readonly DbSet<T> _dbSet;

        // Constructor receives DbContext instance via dependency injection
        public Repository(MmappContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); 
        }

        // Retrieves all entities of type T asynchronously without tracking
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // AsNoTracking improves performance for read-only queries by disabling change tracking
            // ToListAsync executes the query and returns the results as a list asynchronously

            return await _dbSet.AsNoTracking().ToListAsync();
            //try
            //{
            //    var entities =  await _dbSet.AsNoTracking().ToListAsync();

            //    return Result<IEnumerable<T>>.Success(entities);
            //}
            //catch (SqlException ex)
            //{
            //    return Result<IEnumerable<T>>.Failure("Database connection failed. Service is unavailable.");
            //}
        }

        // Finds an entity by its primary key asynchronously
        public async Task<T?> GetByIdAsync(long id)
        {
            // FindAsync searches the database for an entity with the given primary key
            // Returns null if no entity is found
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsyncInclude(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply all include properties dynamically
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstAsync();
        }

        // Adds a new entity to the context asynchronously
        public async Task AddAsync(T entity)
        {
            // AddAsync adds the entity to the DbSet and marks it as Added in the change tracker
            // Actual insertion happens when SaveChangesAsync is called
            await _dbSet.AddAsync(entity);
        }

        // Updates an existing entity in the context
        public void Update(T entity)
        {
            // Update marks the entity state as Modified in the change tracker
            // Changes are persisted to the database upon SaveChangesAsync call
            _dbSet.Update(entity);
        }

        // Deletes an entity from the context
        public void Delete(T entity)
        {
            // Remove marks the entity state as Deleted in the change tracker
            // Entity is removed from the database after SaveChangesAsync is called
            _dbSet.Remove(entity);
        }

        // Checks asynchronously if an entity with the given id exists in the database
        public async Task<bool> ExistsAsync(int id)
        {
            // Reuse GetByIdAsync method to try fetching the entity
            var entity = await GetByIdAsync(id);
            // Return true if entity is found, otherwise false
            return entity != null;
        }

        // Saves all changes made in the context to the database asynchronously
        public async Task SaveAsync()
        {
            // SaveChangesAsync commits all Insert, Update, Delete operations tracked by DbContext to the database
            await _context.SaveChangesAsync();
        }
    }
}
