using ComputerStore.BusinessLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IItemsRepository : IRepository<Item>
    {
        Task<List<Item>> FindAll(string searchValue);
    }
}
