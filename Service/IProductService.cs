using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public interface IProductService
    {
        public interface IProductService
        {
            Task<IEnumerable<Product>> GetAll();
            Task<Product> GetById(int id);
            Task Create(Product product);
            Task Update(Product product);
            Task Delete(int id);
            Task<IEnumerable<Ingredient>> GetAllIngredients();
        }
    }
}
