using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Models;
using Settimana_3_Manuel.Service;

namespace Settimana_3_Manuel.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IngredientService _ingredientService;

        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientService.GetAll();
            return View(ingredients);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientService.Create(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ingredientService.Update(ingredient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _ingredientService.GetById(ingredient.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _ingredientService.GetById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ingredientService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
