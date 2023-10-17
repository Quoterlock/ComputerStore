using ComputerStore.Models.Domains;

namespace ComputerStore.ViewModels
{
    public class CategoryFormModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? IconAlt { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
    }
}
