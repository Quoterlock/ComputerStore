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
        Task RemoveAsync(string id);
        Task UpdateAsync(ItemModel item);
        Task AddAsync(ItemModel item);
        List<ItemModel> Sort(List<ItemModel> items, Utilities.SortMode sort);
        Task<List<ItemModel>> SearchAsync(string value);
    }
}
