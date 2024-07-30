using System.ComponentModel.DataAnnotations;

namespace Settimana_3_Manuel.Models
{
    public class ProductCreateViewModel
    {

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [Range(0, 60)]
        public int DeliveryTimeInMinutes { get; set; }

        public IFormFile Photo { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        [Required]
        public int[] SelectedIngredients { get; set; }
    }
}
