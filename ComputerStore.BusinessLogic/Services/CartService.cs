using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class CartService : ICartService
    {

        public Task AddItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task Clear(string userId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ItemModel, int> GetItems(string userId)
        {
            throw new NotImplementedException();
        }

        public Task GetTotalCost(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItem(string userId, string itemId)
        {
            throw new NotImplementedException();
        }
    }
}
