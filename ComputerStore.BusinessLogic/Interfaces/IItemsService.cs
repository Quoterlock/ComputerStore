using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface IItemsService
    {
        Task<List<ItemModel>> GetAllAsync();
        Task<ItemModel> GetByIdAsync(string id);
        Task<List<ItemModel>> GetFromCategoryAsync(string categoryId);
        Task<List<ItemModel>> SearchAsync(string value);
        Task RemoveAsync(string id);
        Task UpdateAsync(ItemModel item);
        Task AddAsync(ItemModel item);
    }
}
