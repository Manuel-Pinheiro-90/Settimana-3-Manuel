using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settimana_3_Manuel.Models;
using Settimana_3_Manuel.Service;

namespace Settimana_3_Manuel.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly IngredientService _ingredientService;

        public ProductController(ProductService productService, IngredientService ingredientService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }


        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            var ingredients = await _ingredientService.GetAll();
            var viewModel = new ProductCreateViewModel
            {
                Ingredients = ingredients.ToList()
            };
            return View(viewModel);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    DeliveryTimeInMinutes = viewModel.DeliveryTimeInMinutes,
                    Ingredients = new List<Ingredient>()
                };

                if (viewModel.Photo != null && viewModel.Photo.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await viewModel.Photo.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        product.Photo = Convert.ToBase64String(fileBytes);
                    }
                }

                if (viewModel.SelectedIngredients != null)
                {
                    foreach (var ingredientId in viewModel.SelectedIngredients)
                    {
                        var ingredient = await _ingredientService.GetById(ingredientId);
                        if (ingredient != null)
                        {
                            product.Ingredients.Add(ingredient);
                        }
                    }
                }

                await _productService.Create(product);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Ingredients = (await _ingredientService.GetAll()).ToList();
            return View(viewModel);
        }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var ingredients = await _ingredientService.GetAll();
            var viewModel = new ProductCreateViewModel
            {
                Name = product.Name,
                Price = product.Price,
                DeliveryTimeInMinutes = product.DeliveryTimeInMinutes,
                Ingredients = ingredients.ToList(),
                SelectedIngredients = product.Ingredients.Select(i => i.Id).ToArray()
            };
            return View(viewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel viewModel)
        {
            if (id != viewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = await _productService.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                product.DeliveryTimeInMinutes = viewModel.DeliveryTimeInMinutes;

                if (viewModel.Photo != null && viewModel.Photo.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await viewModel.Photo.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        product.Photo = Convert.ToBase64String(fileBytes);
                    }
                }

                product.Ingredients.Clear();
                if (viewModel.SelectedIngredients != null)
                {
                    foreach (var ingredientId in viewModel.SelectedIngredients)
                    {
                        var ingredient = await _ingredientService.GetById(ingredientId);
                        if (ingredient != null)
                        {
                            product.Ingredients.Add(ingredient);
                        }
                    }
                }

                await _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Ingredients = (await _ingredientService.GetAll()).ToList();
            return View(viewModel);
        }



        // //////////////////////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);




        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }








    }











    }
