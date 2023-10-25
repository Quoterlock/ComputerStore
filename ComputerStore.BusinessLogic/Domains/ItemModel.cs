using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogic.Domains
{
    public class ItemModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public CategoryModel? Category { get; set; }
        public ImageModel? Image { get; set; }
    }
}
