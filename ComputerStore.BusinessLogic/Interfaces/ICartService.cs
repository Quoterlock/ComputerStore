using ComputerStore.BusinessLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task AddItemAsync(string userId, string itemId);
        Task RemoveItemAsync(string userId, string itemId);
        Task<Dictionary<ItemModel, int>> GetItemsAsync(string userId);
        Task<int> GetTotalCostAsync(string userId);
        Task ClearAsync(string userId);
        Task MakeOrderAsync(OrderModel order, string userId);
        Task RemoveAllByIdAsync(string userId, string itemId);
    }
}
