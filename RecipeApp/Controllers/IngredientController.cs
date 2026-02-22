using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: Ingredient/Index
        public IActionResult Index()
        {
            var ingredients = _ingredientRepository.GetAll();

            var viewModel = ingredients.Select(i => new IngredientViewModel
            {
                Id = i.Id,
                Name = i.Name,
                ImageUrl = i.ImageUrl,
                DefaultUnit = i.DefaultUnit,
                CategoryId = i.CategoryId,
                CategoryName = i.Category?.Name ?? "N/A",
                RecipeCount = i.RecipeIngredients.Count,
                PantryItemCount = i.PantryItems.Count
            }).ToList();

            return View(viewModel);
        }

        // GET: Ingredient/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = _ingredientRepository.GetById(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }

            var viewModel = new IngredientViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                ImageUrl = ingredient.ImageUrl,
                DefaultUnit = ingredient.DefaultUnit,
                CategoryId = ingredient.CategoryId,
                CategoryName = ingredient.Category?.Name ?? "N/A",
                RecipeCount = ingredient.RecipeIngredients.Count,
                PantryItemCount = ingredient.PantryItems.Count
            };

            return View(viewModel);
        }

        // GET: Ingredient/Create
        public IActionResult Create()
        {
            var viewModel = new IngredientViewModel();
            PopulateCategoryOptions(viewModel);
            return View(viewModel);
        }

        // POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IngredientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = new Ingredient
                {
                    Name = viewModel.Name,
                    ImageUrl = viewModel.ImageUrl,
                    DefaultUnit = viewModel.DefaultUnit,
                    CategoryId = viewModel.CategoryId
                };

                _ingredientRepository.Add(ingredient);
                _ingredientRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoryOptions(viewModel);
            return View(viewModel);
        }

        // GET: Ingredient/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = _ingredientRepository.GetById(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }

            var viewModel = new IngredientViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                ImageUrl = ingredient.ImageUrl,
                DefaultUnit = ingredient.DefaultUnit,
                CategoryId = ingredient.CategoryId
            };

            PopulateCategoryOptions(viewModel);
            return View(viewModel);
        }

        // POST: Ingredient/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IngredientViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ingredient = _ingredientRepository.GetById(id);
                if (ingredient == null)
                {
                    return NotFound();
                }

                ingredient.Name = viewModel.Name;
                ingredient.ImageUrl = viewModel.ImageUrl;
                ingredient.DefaultUnit = viewModel.DefaultUnit;
                ingredient.CategoryId = viewModel.CategoryId;

                _ingredientRepository.Update(ingredient);
                _ingredientRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoryOptions(viewModel);
            return View(viewModel);
        }

        // GET: Ingredient/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = _ingredientRepository.GetById(id.Value);
            if (ingredient == null)
            {
                return NotFound();
            }

            var viewModel = new IngredientViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                ImageUrl = ingredient.ImageUrl,
                DefaultUnit = ingredient.DefaultUnit,
                CategoryId = ingredient.CategoryId,
                CategoryName = ingredient.Category?.Name ?? "N/A",
                RecipeCount = ingredient.RecipeIngredients.Count,
                PantryItemCount = ingredient.PantryItems.Count
            };

            return View(viewModel);
        }

        // POST: Ingredient/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ingredientRepository.Delete(id);
            _ingredientRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCategoryOptions(IngredientViewModel viewModel)
        {
            viewModel.CategoryOptions = _ingredientRepository.GetCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
        }
    }
}
