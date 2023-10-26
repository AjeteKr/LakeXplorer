using LakeXplorer.Context;
using LakeXplorer.Models;
using Microsoft.EntityFrameworkCore;

namespace LakeXplorer.Repository
{
    // Generic repository class for interacting with entities in the database.
    public class Repository<T> : IRepository<T> where T : BaseModels

    {

        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)

        {
            _context = context;

        }

        // Add a new entity to the repository and save changes.
        public async Task<T> Add(T entity)
        {

            _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        // Delete an entity by its unique identifier and save changes.
        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve an entity by its unique identifier.
        public async Task<T?> Get(int id, CancellationToken token)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(f => f.Id == id, token);
        }

        // Retrieve all entities of type T as an IQueryable collection for querying and filtering.
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();

        }

        // Save changes to the repository asynchronously (not yet implemented).
        public Task SaveAsynk(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        // Update an existing entity in the repository and save changes.
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Authenticate a user by username and password.
        public async Task<Users?> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
