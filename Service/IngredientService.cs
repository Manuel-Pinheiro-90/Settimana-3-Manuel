using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Context;
using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public class IngredientService :IIngredientService
    {
        private readonly DataContext _context;

        public IngredientService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetById(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task Create(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var ingredient = await GetById(id);
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
               



            }
            catch (Exception ex) {throw new Exception("no elimin",ex); }
        }
    }
}
