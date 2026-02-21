using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Category/Index
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();

            var viewModel = categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IconUrl = c.IconUrl,
                IngredientCount = c.Ingredients.Count
            }).ToList();

            return View(viewModel);
        }

        // GET: Category/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IconUrl = category.IconUrl,
                IngredientCount = category.Ingredients.Count
            };

            return View(viewModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    IconUrl = viewModel.IconUrl
                };

                _categoryRepository.Add(category);
                _categoryRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Category/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IconUrl = category.IconUrl
            };

            return View(viewModel);
        }

        // POST: Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = _categoryRepository.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                category.IconUrl = viewModel.IconUrl;

                _categoryRepository.Update(category);
                _categoryRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Category/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryRepository.GetById(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            var viewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IconUrl = category.IconUrl,
                IngredientCount = category.Ingredients.Count
            };

            return View(viewModel);
        }

        // POST: Category/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}