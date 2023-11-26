using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputerStore.DataAccess.Entities
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Brief { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public string CategoryID { get; set; } = string.Empty;
        public byte[]? ImageBytes { get; set; } = Array.Empty<byte>();
    }
}
