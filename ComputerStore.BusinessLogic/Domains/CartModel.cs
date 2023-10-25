using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.BusinessLogic.Domains
{
    public class CartModel
    {
        public string? Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}
