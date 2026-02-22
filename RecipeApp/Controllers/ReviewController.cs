using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IRecipeRepository _recipeRepo;
        private readonly IAppUserRepository _userRepo;

        public ReviewController(IReviewRepository reviewRepo, IRecipeRepository recipeRepo, IAppUserRepository userRepo)
        {
            _reviewRepo = reviewRepo;
            _recipeRepo = recipeRepo;
            _userRepo = userRepo;
        }

        // READ: Get all reviews for a specific recipe
        public IActionResult Index(int recipeId)
        {
            // Verify the recipe exists
            Recipe recipe = _recipeRepo.GetById(recipeId);
            if (recipe == null) return NotFound();

            // Pass the recipe name to the view via ViewBag so the user knows what they are looking at
            ViewBag.RecipeTitle = recipe.Title;
            ViewBag.RecipeId = recipe.Id;

            IEnumerable<Review> reviews = _reviewRepo.GetReviewsByRecipeId(recipeId);
            return View(reviews);
        }

        // READ: Details (View a single review)
        public IActionResult Details(int id)
        {
            Review review = _reviewRepo.GetById(id); 
            if (review == null) return NotFound();

            ReviewDetailsViewModel viewModel = new ReviewDetailsViewModel
            {
                Id = review.Id,
                RecipeTitle = review.Recipe?.Title ?? "Unknown",
                ReviewerName = review.AppUser?.UserName ?? "Unknown",
                Rating = review.Rating,
                Comment = review.Comment,
                DatePosted = review.DatePosted,
                RecipeId = review.RecipeId
            };

            return View(viewModel);
        }

        // CREATE: Show form (Requires the Recipe ID)
        public IActionResult Create(int recipeId, int appUserId)
        {
            Recipe recipe = _recipeRepo.GetById(recipeId);
            if (recipe == null) return NotFound();

            var viewModel = new CreateReviewViewModel
            {
                RecipeId = recipeId,
                RecipeTitle = recipe.Title,
                SelectedAppUserId = appUserId,
                Users = _userRepo.GetAll()
            };

            return View(viewModel);
        }

        // CREATE: Process submission
        [HttpPost]
        public IActionResult Create(CreateReviewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    RecipeId = viewModel.RecipeId,
                    AppUserId = viewModel.SelectedAppUserId,
                    Rating = viewModel.Rating,
                    Comment = viewModel.Comment,
                    DatePosted = DateTime.Now
                };

                _reviewRepo.Add(review);
                _reviewRepo.Save();
                return RedirectToAction(nameof(Index), new { recipeId = review.RecipeId });
            }

            viewModel.Users = _userRepo.GetAll(); // repopulate on failure
            viewModel.RecipeTitle = _recipeRepo.GetById(viewModel.RecipeId)?.Title ?? string.Empty;
            return View(viewModel);
        }

        // UPDATE: Show the populated form
        public IActionResult Edit(int id)
        {
            Review review = _reviewRepo.GetById(id);
            if (review == null) return NotFound();
            return View(review);

        }

        // UPDATE: Process the form submission
        [HttpPost]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewRepo.Update(review);
                _reviewRepo.Save();
                // Redirect back to the list of reviews for THIS specific recipe
                return RedirectToAction(nameof(Index), new { recipeId = review.RecipeId });
            }
            return View(review);
        }

        // DELETE: Show the confirmation page
        public IActionResult Delete(int id)
        {
            Review review = _reviewRepo.GetById(id);
            if (review == null) return NotFound();

            return View(review);
        }

        // DELETE: Process the deletion
        [HttpPost, ActionName("Delete")]
        public IActionResult SaveDelete(int id)
        {
            Review review = _reviewRepo.GetById(id);
            if (review == null) return NotFound();
            _reviewRepo.Delete(id);
            _reviewRepo.Save();
            // Redirect back to the list of reviews for THIS specific recipe
            return RedirectToAction(nameof(Index), new { recipeId = review.RecipeId });
        }
    }
}
