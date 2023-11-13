using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.DataAccess.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PostOfficeAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int TotalCost { get; set; } = 0;
        public List<string> ItemsID { get; set; } = new List<string>();
        public string Status { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateTime { get; set;}
        public string CustomerComment { get; set; } = string.Empty;
    }
}
