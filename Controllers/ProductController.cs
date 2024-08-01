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
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;

        public ProductController(IProductService productService, IIngredientService ingredientService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
        }

        // ///////////////////////////////////////GET ALL///////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }


        // //////////////////////////////////////CREATE///////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Create()
        {
            var ingredients = await _ingredientService.GetAll();
            var viewModel = new ProductCreateViewModel
            {
                Ingredients = ingredients.ToList()
            };
            return View(viewModel);
        }

       
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


        // ///////////////////////////////////////EDIT///////////////////////////////////////////////////////////////////


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



        // ////////////////////////////////////////DELETE//////////////////////////////////////////////////////////


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);




        }

        // ///////////////////////////////////////GetProductIMAGE/////////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> GetProductImage(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null || string.IsNullOrEmpty(product.Photo))
            {
                return NotFound();
            }

            byte[] imageBytes = Convert.FromBase64String(product.Photo);
            return File(imageBytes, "image/png");
        }


        // ///////////////////////////////////////DELETE///////////////////////////////////////////////////////////////////

        [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.Delete(id);
        return RedirectToAction(nameof(Index));
    }




}



    }











    
