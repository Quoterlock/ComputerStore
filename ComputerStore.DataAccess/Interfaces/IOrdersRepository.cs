using ComputerStore.DataAccess.Entities;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        
        /*
        Task Add(Order order);
        Task ChangeStatus(string status);
        */
    }
}
