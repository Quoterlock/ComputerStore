using ComputerStore.Models.Domains;

namespace ComputerStore.Models.Interfaces
{
    public interface IOrdersManager
    {
        Task Add(Order order);
        Task ChangeStatus(string status);
    }
}
