using ComputerStore.BusinessLogic.Domains;
using ComputerStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface IOrdersService
    {
        Task Add(OrderModel order);
        Task Update(OrderModel order);
        Task Delete(string orderId);
        Task<OrderModel> GetById(string id);
        Task<List<OrderModel>> GetAll();
        Task SetStatus(string id, string status);
        Task RemoveItem(string itemId, string orderId);
        Task AddItem(string itemId, string orderId);
        Task<List<OrderModel>> SearchAsync(string value);
        Task<List<OrderModel>> GetByStatus(string status);
    }
}
