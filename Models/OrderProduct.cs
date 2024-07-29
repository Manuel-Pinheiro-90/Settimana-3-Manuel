using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Settimana_3_Manuel.Models
{
    public class OrderProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required Product Product { get; set; }
        public required Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
