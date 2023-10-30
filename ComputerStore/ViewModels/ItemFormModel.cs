using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.ViewModels
{
    public class ItemFormModel
    {
        public ItemModel? Item { get; set; }
        public List<CategoryModel>? Categories { get; set; }
        public string? SelectedCategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
