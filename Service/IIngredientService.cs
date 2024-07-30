using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAll();
        Task<Ingredient> GetById(int id);
        Task Create(Ingredient ingredient);
        Task Update(Ingredient ingredient);
        Task Delete(int id);
    }
}
