using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Context;
using Settimana_3_Manuel.Models;

namespace Settimana_3_Manuel.Service
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService (DataContext context)
        {
            _context = context;
        }
        // //////////////////////////////////////GET ALL///////////////////////////////////////////////////////
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(p => p.Ingredients).ToListAsync();
        }
        // //////////////////////////////////////GET ID///////////////////////////////////////////////////////
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(p => p.Ingredients).FirstOrDefaultAsync(p => p.Id == id);
        }
        // //////////////////////////////////////CREATE///////////////////////////////////////////////////////
        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        // //////////////////////////////////////UPDATE///////////////////////////////////////////////////////
        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        // //////////////////////////////////////Delete///////////////////////////////////////////////////////
        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        // //////////////////////////////////////GET ALL INGREDIENT///////////////////////////////////////////////////////
        public async Task<IEnumerable<Ingredient>> GetAllIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }
    }
}
