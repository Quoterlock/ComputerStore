using ComputerStore.Models.Domains;

namespace ComputerStore.Models.Interfaces
{
    public interface IUserCartManager
    {
        Task<UserCart> Get(string userId);
        Task AddItem(string userId, string itemId);
        Task Clear(string userId);
        Task DeleteItem(string userId, string ItemId);
    }
}
