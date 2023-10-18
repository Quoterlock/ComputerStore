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
        Task AddItem(string userId, string itemId);
        Task RemoveItem(string userId, string itemId);
        Dictionary<Item, int> GetItems(string userId);
        Task GetTotalCost(string userId);
        Task Clear(string userId);
    }
}
