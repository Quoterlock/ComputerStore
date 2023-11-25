using System.Net;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAsync(string id);
        Task<List<TEntity>> GetAsync(Func<TEntity, bool> predicate);
        Task<List<TEntity>> GetAsync();
        Task AddAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(string id);
        Task<bool> IsExistsAsync(string id);
    }
}
