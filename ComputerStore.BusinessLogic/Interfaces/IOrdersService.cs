using ComputerStore.BusinessLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface IOrdersService
    {
        Task MakeOrder(OrderModel order);
    }
}
