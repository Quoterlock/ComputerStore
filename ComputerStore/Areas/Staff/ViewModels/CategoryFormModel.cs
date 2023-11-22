using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.Areas.Staff.ViewModels
{
    public class CategoryFormModel
    {
        public CategoryModel? Category { get; set; } = new CategoryModel();
        public IFormFile? ThumbnailFile { get; set; }
    }
}
