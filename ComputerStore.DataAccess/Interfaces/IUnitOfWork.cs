using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IUserCartRepository UserCart { get; set; }
        IItemsRepository Items { get; set; }
        ICategoriesRepository Categories { get; set; }
        IOrdersRepository Orders { get; set; }
        Task CommitAsync();
        Task Rollback();
    }
}
