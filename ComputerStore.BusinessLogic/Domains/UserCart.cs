using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.BusinessLogic.Domains
{
    public class UserCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
