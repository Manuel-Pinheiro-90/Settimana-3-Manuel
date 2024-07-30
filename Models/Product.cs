using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Settimana_3_Manuel.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
      
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public  string Photo { get; set; }


        public decimal Price { get; set; }
     
        [Range(0, 60)]
        public int DeliveryTimeInMinutes { get; set; }
        public List<Ingredient> Ingredients { get; set; } = [];
        public List<OrderProduct> OrderProducts { get; set; } = [];

    }
}
