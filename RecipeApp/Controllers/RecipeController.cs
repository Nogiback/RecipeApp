using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: Recipe/Index
        public IActionResult Index()
        {
            var recipes = _recipeRepository.GetAll();

            var viewModel = recipes.Select(r => new RecipeViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Instructions = r.Instructions,
                ImageUrl = r.ImageUrl,
                CookTimeMins = r.CookTimeMins,
                AppUserId = r.AppUserId,
                AppUserName = r.AppUser?.UserName ?? "N/A",
                IngredientCount = r.RecipeIngredients.Count,
                ReviewCount = r.Reviews.Count
            }).ToList();

            return View(viewModel);
        }

        // GET: Recipe/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _recipeRepository.GetById(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                CookTimeMins = recipe.CookTimeMins,
                AppUserId = recipe.AppUserId,
                AppUserName = recipe.AppUser?.UserName ?? "N/A",
                IngredientCount = recipe.RecipeIngredients.Count,
                ReviewCount = recipe.Reviews.Count,
                IngredientItems = recipe.RecipeIngredients.Select(ri => new RecipeIngredientItemViewModel
                {
                    IngredientName = ri.Ingredient?.Name ?? "N/A",
                    Quantity = ri.Quantity,
                    Unit = ri.UnitDisplay
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            var viewModel = new RecipeViewModel();
            PopulateAppUserOptions(viewModel);
            return View(viewModel);
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RecipeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe
                {
                    Title = viewModel.Title,
                    Instructions = viewModel.Instructions,
                    ImageUrl = viewModel.ImageUrl,
                    CookTimeMins = viewModel.CookTimeMins,
                    AppUserId = viewModel.AppUserId
                };

                _recipeRepository.Add(recipe);
                _recipeRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            PopulateAppUserOptions(viewModel);
            return View(viewModel);
        }

        // GET: Recipe/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _recipeRepository.GetById(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                CookTimeMins = recipe.CookTimeMins,
                AppUserId = recipe.AppUserId
            };

            PopulateAppUserOptions(viewModel);
            return View(viewModel);
        }

        // POST: Recipe/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RecipeViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipe = _recipeRepository.GetById(id);
                if (recipe == null)
                {
                    return NotFound();
                }

                recipe.Title = viewModel.Title;
                recipe.Instructions = viewModel.Instructions;
                recipe.ImageUrl = viewModel.ImageUrl;
                recipe.CookTimeMins = viewModel.CookTimeMins;
                recipe.AppUserId = viewModel.AppUserId;

                _recipeRepository.Update(recipe);
                _recipeRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            PopulateAppUserOptions(viewModel);
            return View(viewModel);
        }

        // GET: Recipe/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = _recipeRepository.GetById(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            var viewModel = new RecipeViewModel
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                CookTimeMins = recipe.CookTimeMins,
                AppUserId = recipe.AppUserId,
                AppUserName = recipe.AppUser?.UserName ?? "N/A",
                IngredientCount = recipe.RecipeIngredients.Count,
                ReviewCount = recipe.Reviews.Count
            };

            return View(viewModel);
        }

        // POST: Recipe/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _recipeRepository.Delete(id);
            _recipeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAppUserOptions(RecipeViewModel viewModel)
        {
            viewModel.AppUserOptions = _recipeRepository.GetAppUsers()
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                });
        }
    }
}
