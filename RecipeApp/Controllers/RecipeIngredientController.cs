using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class RecipeIngredientController : Controller
    {
        private readonly IRecipeRepository _recipeRepo;
        private readonly IIngredientRepository _ingredientRepo;
        private readonly IRecipeIngredientRepository _recipeIngredientRepo;

        public RecipeIngredientController(IRecipeRepository recipeRepo, IIngredientRepository ingredientRepo, IRecipeIngredientRepository recipeIngredientRepo)
        {
            _recipeRepo = recipeRepo;
            _ingredientRepo = ingredientRepo;
            _recipeIngredientRepo = recipeIngredientRepo;
        }

        public IActionResult Index(int recipeId)
        {
            Recipe recipe = _recipeRepo.GetById(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }

            ViewBag.RecipeTitle = recipe.Title;
            ViewBag.RecipeId = recipeId;

            IEnumerable<RecipeIngredient> ingredients =
                _recipeIngredientRepo.GetIngredientsByRecipeId(recipe.Id);
            return View(ingredients);
        }
        // CREATE: Show form 
        public IActionResult Create(int recipeId)
        {
            var recipe =  _recipeRepo.GetById(recipeId);
            if (recipe == null) return NotFound();

            ViewBag.RecipeTitle = recipe.Title;

            var allIngredients =  _ingredientRepo.GetAll();

            var viewModel = new CreateRecipeIngredientViewModel
            {
                RecipeId = recipeId, // Lock in the recipe
                IngredientOptions = allIngredients.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Name} ({i.DefaultUnit})" // Show the unit in the dropdown for better UX!
                })
            };

            return View(viewModel);
        }

        // CREATE: Process submission
        [HttpPost]
        public IActionResult Create(CreateRecipeIngredientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                RecipeIngredient newLink = new RecipeIngredient
                {
                    RecipeId = viewModel.RecipeId,
                    IngredientId = viewModel.SelectedIngredientId,
                    Quantity = viewModel.Quantity,
                    UnitDisplay = viewModel.UnitDisplay
                };

                 _recipeIngredientRepo.Add(newLink);
                _recipeIngredientRepo.Save();

                return RedirectToAction(nameof(Index), new { recipeId = viewModel.RecipeId });
            }

            // Repopulate dropdown if validation fails
            IEnumerable<Ingredient> allIngredients =  _ingredientRepo.GetAll();
            viewModel.IngredientOptions = allIngredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Name} ({i.DefaultUnit})"
            });

            return View(viewModel);
        }

        // UPDATE: Show the populated form (GET)
        public IActionResult Edit(int id)
        {
            RecipeIngredient recipeIngredient = _recipeIngredientRepo.GetById(id);
            if (recipeIngredient == null) return NotFound();

            // We still need the dropdown list so the user can change the ingredient!
            IEnumerable<Ingredient> allIngredients =  _ingredientRepo.GetAll();

            CreateRecipeIngredientViewModel viewModel = new CreateRecipeIngredientViewModel
            {
                Id = recipeIngredient.Id,
                RecipeId = recipeIngredient.RecipeId,
                SelectedIngredientId = recipeIngredient.IngredientId,
                Quantity = recipeIngredient.Quantity,
                UnitDisplay = recipeIngredient.UnitDisplay,
                IngredientOptions = allIngredients.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Name} ({i.DefaultUnit})"
                })
            };

            return View(viewModel);
        }
        // UPDATE: Process the submission (POST)
        [HttpPost]
        public IActionResult Edit(int id, CreateRecipeIngredientViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                RecipeIngredient existingLink =  _recipeIngredientRepo.GetById(id);
                if (existingLink == null) return NotFound();

                // Update the properties
                existingLink.IngredientId = viewModel.SelectedIngredientId;
                existingLink.Quantity = viewModel.Quantity;
                existingLink.UnitDisplay = viewModel.UnitDisplay;

                _recipeIngredientRepo.Update(existingLink);
                _recipeIngredientRepo.Save();

                return RedirectToAction(nameof(Index), new { recipeId = viewModel.RecipeId });
            }

            // If validation fails, reload the dropdown
            IEnumerable<Ingredient> allIngredients = _ingredientRepo.GetAll();
            viewModel.IngredientOptions = allIngredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Name} ({i.DefaultUnit})"
            });

            return View(viewModel);
        }
        // DELETE: Show the confirmation page (GET)
        public IActionResult Delete(int id)
        {
            RecipeIngredient recipeIngredient = _recipeIngredientRepo.GetById(id);
            if (recipeIngredient == null) return NotFound();

            return View(recipeIngredient);
        }

        // DELETE: Process the deletion (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            RecipeIngredient recipeIngredient = _recipeIngredientRepo.GetById(id);
            if (recipeIngredient != null)
            {
                var recipeId = recipeIngredient.RecipeId; // Save this for the redirect

                _recipeIngredientRepo.Delete(id);
                _recipeIngredientRepo.Save();

                return RedirectToAction(nameof(Index), new { recipeId = recipeId });
            }

            return NotFound();
        }
    }
}
