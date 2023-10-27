using ComputerStore.BusinessLogic.Domains;

namespace ComputerStore.Areas.Staff.ViewModels
{
    public class CategoryFormModel
    {
        public CategoryModel? Category { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
    }
}
