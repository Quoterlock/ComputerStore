using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DataAccess.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        [ForeignKey("ImageId")]
        public Image? Image { get; set; }
    }
}
