using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.ViewModels
{
    public class OrderFormViewModel
    {
        public Dictionary<ItemModel, int> Items { get; set; } = new Dictionary<ItemModel, int>();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PostOfficeAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int TotalCost { get; set; } = 0;

    }
}
