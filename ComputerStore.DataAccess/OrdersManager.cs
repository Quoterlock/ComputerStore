using ComputerStore.Models.Domains;
using ComputerStore.Models.Interfaces;

namespace ComputerStore.Models
{
    public class OrdersManager : IOrdersManager
    {
        public Task Add(Order order)
        {
            throw new NotImplementedException();
        }

        public Task ChangeStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
