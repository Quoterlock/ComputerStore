using ComputerStore.Models.Domains;
using System.Net;

namespace ComputerStore.Models.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> Get(Func<TEntity, bool> predicate);
        Task<List<TEntity>> GetAll();
        Task Add(TEntity item);
        Task Update(TEntity item);
        Task Delete(string id);
        bool IsValid(TEntity item);
        Task<List<TEntity>> FindAll(string value);
    }
}
