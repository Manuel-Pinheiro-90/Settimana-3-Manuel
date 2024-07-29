using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public interface IProductService : IGenericService<Product>
    {
    
        IEnumerable<Ingredient> GetAllIngredients();
      


    }
}
