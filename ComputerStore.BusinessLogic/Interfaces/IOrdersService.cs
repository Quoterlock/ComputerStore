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
        Task AddAsync(OrderModel order);
        Task UpdateAsync(OrderModel order);
        Task DeleteAsync(string orderId);
        Task<OrderModel> GetById(string id);
        Task<List<OrderModel>> GetAll();
        Task SetStatusAsync(string id, string status);
        Task RemoveItemAsync(string itemId, string orderId);
        Task AddItemAsync(string itemId, string orderId);
        Task<List<OrderModel>> SearchAsync(string value);
        Task<List<OrderModel>> GetByStatus(string status);
    }
}
