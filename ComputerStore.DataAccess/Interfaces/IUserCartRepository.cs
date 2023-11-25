using ComputerStore.DataAccess.Entities;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IUserCartRepository : IRepository<UserCart>
    {
        Task RemoveItems(string userId);
        Task<UserCart> GetUserCartAsync(string userId);
    }
}
