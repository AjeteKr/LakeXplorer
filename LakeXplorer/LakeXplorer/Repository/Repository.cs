﻿using LakeXplorer.Context;
using LakeXplorer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LakeXplorer.Repository
{
    // Generic repository class for interacting with entities in the database.
    public class Repository<T> : IRepository<T> where T : BaseModels

    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)

        {
            _context = context;
            _logger = logger;

        }

        // Add a new entity to the repository and save changes.
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
