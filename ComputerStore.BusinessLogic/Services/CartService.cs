using ComputerStore.BusinessLogic.Adapters;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrdersService _ordersService;
        private readonly IEntityToModelAdapter<Item, ItemModel> _itemAdapter;

        public CartService(IUnitOfWork unitOfWork, 
            IOrdersService ordersService,
            IEntityToModelAdapter<Item, ItemModel> itemAdapter)
        {
            _unitOfWork = unitOfWork;
            _ordersService = ordersService;
            _itemAdapter = itemAdapter;
        }

        public async Task AddItem(string userId, string itemId)
        {
            try
            {
                var cart = await _unitOfWork.UserCart.GetUserCart(userId);
                if (await _unitOfWork.Items.IsExists(itemId))
                {
                    cart.ItemsIDs.Add(itemId);
                    await _unitOfWork.CommitAsync();
                }
                else throw new Exception("Item doesn't exist: " + itemId);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dictionary<ItemModel, int>> GetItems(string userId)
        {
            if(string.IsNullOrEmpty(userId)) 
                throw new ArgumentNullException("user Id");
            try
            {
                var cart = await _unitOfWork.UserCart.GetUserCart(userId);
                var items = new Dictionary<ItemModel, int>();

                foreach (var itemID in cart.ItemsIDs)
                {
                    var entity = await _unitOfWork.Items.GetAsync(itemID);
                    var itemModel = _itemAdapter.ToModel(entity);
                    if (items.Keys.Any(k => k.Id == itemModel.Id))
                        items[items.Keys.FirstOrDefault(k => k.Id == itemModel.Id)]++;
                    else
                        items.Add(itemModel, 1);
                }
                return items;

            } catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetTotalCost(string userId)
        {
            if (string.IsNullOrEmpty(userId)) 
                throw new ArgumentNullException("userId");
            
            var cart = await _unitOfWork.UserCart.GetUserCart(userId);
            int sum = 0;
            foreach(var itemId in cart.ItemsIDs)
            {
                var item = await _unitOfWork.Items.GetAsync(itemId);
                if (item != null)
                    sum += item.Price;
            }
            return sum;
        }

        public async Task RemoveItem(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            var cart = await _unitOfWork.UserCart.GetUserCart(userId);
            cart.ItemsIDs.Remove(itemId);
            await _unitOfWork.CommitAsync();
        }

        public async Task Clear(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            await _unitOfWork.UserCart.RemoveItems(userId);
            await _unitOfWork.CommitAsync();
        }

        public async Task MakeOrder(OrderModel order, string userId)
        {
            if (!string.IsNullOrEmpty(userId) && order != null)
            {
                order.Items = await GetItems(userId);
                if (order.Items.Count <= 0)
                    throw new Exception("Items count is null");

                order.CreationDate = DateTime.Now.ToUniversalTime();
                order.LastUpdateTime = DateTime.Now.ToUniversalTime();

                try
                {
                    await _ordersService.Add(order);
                    await Clear(userId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("order or userId is null");
        }

        public async Task RemoveAllById(string userId, string itemId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(itemId))
                throw new ArgumentNullException("itemId");

            var cart = await _unitOfWork.UserCart.GetUserCart(userId);
            cart.ItemsIDs.RemoveAll(id => id == itemId);
            await _unitOfWork.CommitAsync();
        }
    }
}
