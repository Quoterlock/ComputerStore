using System.Net;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> Get(Func<TEntity, bool> predicate);
        Task<List<TEntity>> Get();
        Task Add(TEntity item);
        Task Update(TEntity item);
        Task Delete(string id);
    }
}
