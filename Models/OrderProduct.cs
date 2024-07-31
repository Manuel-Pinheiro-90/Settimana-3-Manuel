using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Settimana_3_Manuel.Models
{
    public class OrderProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public  Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
