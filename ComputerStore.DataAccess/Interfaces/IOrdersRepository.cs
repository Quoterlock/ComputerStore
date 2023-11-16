using ComputerStore.DataAccess.Entities;

namespace ComputerStore.DataAccess.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Task UpdateInfoAsync(Order order);
        /*
        Task Add(Order order);
        Task ChangeStatus(string status);
        */
    }
}
