using ComputerStore.BusinessLogic.Domains;
using ComputerStore.Utilities;
using NuGet.Protocol.Core.Types;

namespace ComputerStore.Areas.Customer.ViewModels
{
    public class ItemsListViewModel
    {
        public List<ItemModel> Items { get; set; } = new List<ItemModel>();
        public int Count { get; set; }
        public string Title { get; set; } = "none";
        public SortMode SortBy { get; set; } = SortMode.ItemId;
        public string Category { get; set; } = string.Empty;
        public string CategoryID { get; set; } = string.Empty;
    }
}
