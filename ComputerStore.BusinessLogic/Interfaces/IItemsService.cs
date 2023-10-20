using ComputerStore.BusinessLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface IItemsService
    {
        Task<List<ItemModel>> GetAll();
        Task<List<ItemModel>> GetFromCategory(string categoryId);
        Task Search(string value);
    }
}
