using ComputerStore.BusinessLogic.Adapters;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using ComputerStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityToModelAdapter<Order, OrderModel> _orderAdapter;
        private readonly IEntityToModelAdapter<Item, ItemModel> _itemAdapter;


        public OrdersService(IUnitOfWork unitOfWork, 
            IEntityToModelAdapter<Order, OrderModel> orderAdapter,
            IEntityToModelAdapter<Item, ItemModel> itemAdapter) 
        {
            _unitOfWork = unitOfWork;
            _orderAdapter = orderAdapter;
            _itemAdapter = itemAdapter;
        }

        public async Task Delete(string orderId)
        {
            if(!string.IsNullOrEmpty(orderId))
            {
                try
                {
                    await _unitOfWork.Orders.DeleteAsync(orderId);
                    await _unitOfWork.CommitAsync();
                } catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<OrderModel>> GetAll()
        {
            var entities = await _unitOfWork.Orders.GetAsync();
            var orders = ConvertEntitiesToModels(entities);
            for (int i = 0; i < orders.Count; i++)
                orders[i].Items = await GetItems(orders[i].Items);
            return orders;
        }

        public async Task<OrderModel> GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    var entity = await _unitOfWork.Orders.GetAsync(id);
                    if (entity != null)
                    {
                        var order = _orderAdapter.ToModel(entity);
                        
                        
                        order.Items = await GetItems(order.Items);
                        return order;
                    }
                    else throw new Exception("Order doesn't exist id: " + id);
                } 
                catch (Exception ex) 
                { 
                    throw new Exception(ex.Message); 
                }
            }
            else throw new ArgumentNullException("order id");
        }

        public async Task Add(OrderModel order)
        {
            if (order != null)
            {
                await _unitOfWork.Orders.AddAsync(_orderAdapter.ToEntity(order));
                await _unitOfWork.CommitAsync();
            }
            else throw new ArgumentNullException("order model");
        }

        public async Task SetStatus(string id, string status)
        {
            if(!string.IsNullOrEmpty(id))
            {
                if (OrderStatuses.StringToEnum(status) == OrderStatus.Unknown)
                    throw new Exception("Unknown status");
                var order = await _unitOfWork.Orders.GetAsync(id);
                order.Status = status.ToString();
                order.LastUpdateTime = DateTime.Now.ToUniversalTime();
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task Update(OrderModel order)
        {
            if (order != null && !string.IsNullOrEmpty(order.Id))
            {
                try
                {
                    await _unitOfWork.Orders.UpdateInfoAsync(_orderAdapter.ToEntity(order));
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else throw new ArgumentNullException("order model");
        }

        private List<OrderModel> ConvertEntitiesToModels(List<Order> entities)
        {
            var models = new List<OrderModel>();
            foreach (var entity in entities)
            {
                models.Add(_orderAdapter.ToModel(entity));
            }
            return models;
        }

        private async Task<Dictionary<ItemModel, int>> GetItems(Dictionary<ItemModel, int> items)
        {
            var newItems = new Dictionary<ItemModel, int>();
            foreach (var itemKey in items)
            {
                var item = _itemAdapter.ToModel(await _unitOfWork.Items.GetAsync(itemKey.Key.Id));
                if(item != null)
                {
                    newItems.Add(item, itemKey.Value);
                }
            }
            return newItems;
        }

        public async Task RemoveItem(string itemId, string orderId)
        {
            if (!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(orderId))
            {
                var order = await _unitOfWork.Orders.GetAsync(orderId);
                order.ItemsID.Remove(itemId);
                await _unitOfWork.CommitAsync();
            }
            else throw new ArgumentNullException("itemID or orderID");
        }

        public async Task AddItem(string itemId, string orderId)
        {
            if (!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(orderId))
            {
                var order = await _unitOfWork.Orders.GetAsync(orderId);
                order.ItemsID.Add(itemId);
                await _unitOfWork.CommitAsync();
            }
            else throw new ArgumentNullException("itemID or orderID");
        }

        public async Task<List<OrderModel>> SearchAsync(string value)
        {
            value = value.ToLower();
            var orders = new List<Order>();
            orders.AddRange(await _unitOfWork.Orders.GetAsync(o => o.Id.ToString().ToLower().Contains(value)));
            orders.AddRange(await _unitOfWork.Orders.GetAsync(o => o.Status.ToString().ToLower().Contains(value)));
            orders.AddRange(await _unitOfWork.Orders.GetAsync(o => o.LastName.ToLower().Contains(value)));
            orders.AddRange(await _unitOfWork.Orders.GetAsync(o => o.FirstName.ToLower().Contains(value)));
            return ConvertEntitiesToModels(orders);
        }

        public async Task<List<OrderModel>> GetByStatus(string status)
        {
            var entities = await _unitOfWork.Orders.GetAsync(o => o.Status.Equals(status));
            return ConvertEntitiesToModels(entities);
        }
    }
}
