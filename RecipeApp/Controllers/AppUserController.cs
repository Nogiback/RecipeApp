using Microsoft.AspNetCore.Mvc;
using RecipeApp.Models;
using RecipeApp.Repository;
using RecipeApp.ViewModels;

namespace RecipeApp.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserRepository _userRepository;

        public AppUserController(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: AppUser/Index
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();

            var viewModel = users.Select(u => new AppUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                ProfilePicUrl = u.ProfilePicUrl,
                JoinedDate = u.JoinedDate,
                RecipeCount = u.Recipes.Count,
                ReviewCount = u.Reviews.Count
            }).ToList();

            return View(viewModel);
        }

        // GET: AppUser/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new AppUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicUrl = user.ProfilePicUrl,
                JoinedDate = user.JoinedDate,
                RecipeCount = user.Recipes.Count,
                ReviewCount = user.Reviews.Count
            };

            return View(viewModel);
        }

        // GET: AppUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    ProfilePicUrl = viewModel.ProfilePicUrl,
                    JoinedDate = DateTime.Now
                };

                _userRepository.Add(user);
                _userRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: AppUser/Edit/{id}
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new AppUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicUrl = user.ProfilePicUrl,
                JoinedDate = user.JoinedDate
            };

            return View(viewModel);
        }

        // POST: AppUser/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppUserViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = _userRepository.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = viewModel.UserName;
                user.Email = viewModel.Email;
                user.ProfilePicUrl = viewModel.ProfilePicUrl;

                _userRepository.Update(user);
                _userRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: AppUser/Delete/{id}
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userRepository.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new AppUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicUrl = user.ProfilePicUrl,
                JoinedDate = user.JoinedDate,
                RecipeCount = user.Recipes.Count,
                ReviewCount = user.Reviews.Count
            };

            return View(viewModel);
        }

        // POST: AppUser/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}