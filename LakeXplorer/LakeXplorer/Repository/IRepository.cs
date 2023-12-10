using LakeXplorer.Models;

namespace LakeXplorer.Repository
{

    // Generic repository interface for interacting with entities
    public interface IRepository<T> 

    {
        Task<T?> Get(int id, CancellationToken token);

        IQueryable<T> GetAll();
        Task<T> Add(T entity);

        void Update(T entity);
        Task Delete(int id);

        Task SaveAsynk(CancellationToken token);

        Task<Users?> GetUserByUsernameAndPassword(string username, string password);

        Task<List<Likes>> GetLikesForUser(int userId);

    }
}