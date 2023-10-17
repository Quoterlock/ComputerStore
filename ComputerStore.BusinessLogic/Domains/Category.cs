using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.BusinessLogic.Domains
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public Image? Thumbnail { get; set; }
    }
}
