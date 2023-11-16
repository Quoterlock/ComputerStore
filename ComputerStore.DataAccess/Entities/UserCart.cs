using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.DataAccess.Entities
{
    public class UserCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<string> ItemsIDs { get; set; } = new List<string>();
    }
}
