using LakeXplorer.Models;

namespace LakeXplorer.Repository
{

    // Generic repository interface for interacting with entities
    public interface IRepository<T> 

    {
        /// <summary>
        /// Retrieve an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <param name="token">A cancellation token to support asynchronous cancellation.</param>
        /// <returns>The entity with the specified ID, or null if not found.</returns>
        Task<T?> Get(int id, CancellationToken token);

        /// <summary>
        /// Retrieve all entities of type T.
        /// </summary>
        /// <returns>An IQueryable collection of entities for querying and filtering.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        Task<T> Add(T entity);

        // <summary>
        /// Update an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Delete an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>An asynchronous task representing the deletion operation.</returns>
        Task Delete(int id);

        /// <summary>
        /// Save changes to the repository asynchronously.
        /// </summary>
        /// <param name="token">A cancellation token to support asynchronous cancellation.</param>
        /// <returns>An asynchronous task representing the save operation.</returns>
        Task SaveAsynk(CancellationToken token);

        /// <summary>
        /// Authenticate a user by username and password.
        /// </summary>
        /// <param name="username">The username of the user to authenticate.</param>
        /// <param name="password">The password of the user to authenticate.</param>
        /// <returns>The authenticated user or null if not found or authentication fails.</returns>
        Task<Users?> GetUserByUsernameAndPassword(string username, string password);

    }
}