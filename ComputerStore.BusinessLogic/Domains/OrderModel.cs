using ComputerStore.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.BusinessLogic.Domains
{
    public class OrderModel
    {
        public string? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PostOfficeAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int TotalCost { get; set; } = 0;
        public Dictionary<ItemModel, int> Items { get; set; } = new Dictionary<ItemModel, int>();
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateTime { get; set;}
        public string CustomerComment { get; set; } = string.Empty;
    }
}
