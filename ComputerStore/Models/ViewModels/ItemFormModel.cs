using ComputerStore.Models.Domains;

namespace ComputerStore.Models.ViewModels
{
    public class ItemFormModel
    {
        public Item? Item { get; set; }
        public List<Category>? Categories { get; set; }
        public string? SelectedCategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
