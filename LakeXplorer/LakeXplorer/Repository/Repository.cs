using LakeXplorer.Context;
using LakeXplorer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LakeXplorer.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModels

    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)

        {
            _context = context;
            _logger = logger;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding entity to database: {ex.Message}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T?> Get(int id, CancellationToken token)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(f => f.Id == id, token);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();

        }

        public Task SaveAsynk(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<Users?> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public async Task<List<Likes>> GetLikesForUser(int userId)
        {
            try
            {
                var likesForUser = await _context.Set<Likes>()
                    .Where(like => like.UserId == userId)
                    .ToListAsync();

                return likesForUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting likes for user with ID {userId}: {ex.Message}");
                throw;
            }
        }

    }
}
