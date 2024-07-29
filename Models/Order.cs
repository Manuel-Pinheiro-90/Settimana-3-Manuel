using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Settimana_3_Manuel.Models
{
    public class Order
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Address { get; set; }
        public string ExtraNote { get; set; }
        public bool Processed { get; set; } = false;   
        public User User { get; set; }
        public List<Product> Products { get; set; } = [];   


    }
}
