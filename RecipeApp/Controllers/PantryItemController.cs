using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class PantryItemController : Controller
    {
        private readonly IIngredientRepository _ingredientRepo;
        private readonly IPantryItemRepository _pantryItemRepo;

        public PantryItemController(IIngredientRepository ingredientRepo, IPantryItemRepository pantryItemRepo)
        {
            _ingredientRepo = ingredientRepo;
            _pantryItemRepo = pantryItemRepo;
        }
        public IActionResult Index(int userId)
        {
            
            ViewBag.CurrentUserId = userId;
            IEnumerable<PantryItem> pantryItems = _pantryItemRepo.GetPantryByUserId(userId);
            return View(pantryItems);
        }

        public IActionResult Create(int userId)
        {
            IEnumerable<Ingredient> ingredients = _ingredientRepo.GetAll();

            CreatePantryItemViewModel viewModel = new CreatePantryItemViewModel
            {
                IngredientOptions = ingredients.Select(
                    i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    }),
                UserId = userId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreatePantryItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the ingredient options for the dropdown
                IEnumerable<Ingredient> ingredients = _ingredientRepo.GetAll();
                viewModel.IngredientOptions = ingredients.Select(
                    i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    });
                return View("Create", viewModel);
            }

            PantryItem newItem = new PantryItem
            {
                AppUserId = viewModel.UserId,
                IngredientId = viewModel.ExistingIngredientId.Value,
                Quantity = viewModel.Quantity,
                ExpiryDate = viewModel.ExpiryDate
            };

            _pantryItemRepo.Add(newItem);
            _pantryItemRepo.Save();

            return RedirectToAction("Index", new { userId = viewModel.UserId });
        }

        // UPDATE: Show the populated form (GET)
        public IActionResult Edit(int id)
        {
            PantryItem pantryItem = _pantryItemRepo.GetById(id);
            if (pantryItem == null) return NotFound();

            // Fetch ingredients for the dropdown
            IEnumerable<Ingredient> ingredients =  _ingredientRepo.GetAll();

            var viewModel = new CreatePantryItemViewModel
            {
                Id = pantryItem.Id,
                ExistingIngredientId = pantryItem.IngredientId,
                Quantity = pantryItem.Quantity,
                ExpiryDate = pantryItem.ExpiryDate,
                IngredientOptions = ingredients.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                })
            };

            return View(viewModel);
        }

        // UPDATE: Process the submission (POST)
        [HttpPost]
        public IActionResult Edit(int id, CreatePantryItemViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                PantryItem existingItem =  _pantryItemRepo.GetById(id);
                if (existingItem == null) return NotFound();

                // Update the tracked entity
                existingItem.IngredientId = viewModel.ExistingIngredientId.Value;
                existingItem.Quantity = viewModel.Quantity;
                existingItem.ExpiryDate = viewModel.ExpiryDate;

                 _pantryItemRepo.Update(existingItem);
                 _pantryItemRepo.Save();

                return RedirectToAction("Index", new { userId = existingItem.AppUserId });
            }

            // If validation fails, reload the dropdown
            IEnumerable<Ingredient> ingredients =  _ingredientRepo.GetAll();
            viewModel.IngredientOptions = ingredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            });

            return View(viewModel);
        }

        // DELETE: Show the confirmation page (GET)
        public IActionResult Delete(int id)
        {
            
            PantryItem pantryItem =  _pantryItemRepo.GetById(id);
            if (pantryItem == null) return NotFound();

            return View(pantryItem);
        }

        // DELETE: Process the deletion (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(PantryItem pantryItem)
        {
             _pantryItemRepo.Delete(pantryItem.Id);
             _pantryItemRepo.Save();

            return RedirectToAction("Index", new { userId = pantryItem.AppUserId });
        }
    }
}

