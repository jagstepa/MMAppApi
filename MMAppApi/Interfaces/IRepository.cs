using Microsoft.AspNetCore.Mvc;
using MMAppApi.DTO;
using MMAppApi.Helpers;
using MMAppApi.Models;
using System.Linq.Expressions;

namespace MMAppApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Asynchronously retrieves all entities of type T as an enumerable collection
        Task<IEnumerable<T>> GetAllAsync();
        // Asynchronously retrieves a single entity by its primary key (int id)
        // Returns null if entity is not found
        Task<T?> GetByIdAsync(long id);
        Task<T> GetByIdAsyncInclude(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        // Asynchronously adds a new entity of type T to the data source
        Task AddAsync(T entity);
        // Updates an existing entity of type T in the data source
        void Update(T entity);
        // Deletes the specified entity of type T from the data source
        void Delete(T entity);
        // Asynchronously checks if an entity with the given id exists in the data source
        // Returns true if found, otherwise false
        Task<bool> ExistsAsync(int id);
        // Asynchronously saves all pending changes (add, update, delete) to the database
        Task SaveAsync();
    }
}
