using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddItem(string userId, string itemId)
        {
            try
            {
                await _unitOfWork.UserCart.AddItem(userId, itemId);
                await _unitOfWork.Commit();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dictionary<ItemModel, int>> GetItems(string userId)
        {
            if(string.IsNullOrEmpty(userId)) 
                throw new ArgumentNullException("userId");
            
            var cart = await _unitOfWork.UserCart.GetUserCart(userId); 
            var items = new Dictionary<ItemModel, int>();
            
            foreach (var item in cart.Items)
            {
                var itemModel = Convertor.EntityToModel(item);
                if (items.ContainsKey(itemModel))
                    items[itemModel]++;
                else 
                    items.Add(itemModel, 1);
            }
            
            return items;
        }

        public async Task<int> GetTotalCost(string userId)
        {
            if (string.IsNullOrEmpty(userId)) 
                throw new ArgumentNullException("userId");
            
            var cart = await _unitOfWork.UserCart.GetUserCart(userId);
            
            int sum = 0;
            foreach(var item in cart.Items)
                sum += item.Price;

            return sum;
        }

        public async Task RemoveItem(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            await _unitOfWork.UserCart.DeleteItem(userId, itemId);
            await _unitOfWork.Commit();
        }

        public async Task Clear(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            await _unitOfWork.UserCart.RemoveItems(userId);
            await _unitOfWork.Commit();
        }
    }
}
