using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IUserCartRepository : IRepository<UserCart>
    {
        Task AddItem(string userId, string itemId);
        Task DeleteItem(string userId, string itemId);
        Task RemoveItems(string userId);
        Task<UserCart> GetUserCart(string userId);
    }
}
