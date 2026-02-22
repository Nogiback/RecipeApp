using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Recipe obj)
        {
            _context.Recipes.Add(obj);
        }

        public void Delete(int id)
        {
            var recipe = _context.Recipes.Find(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }
        }

        public void Update(Recipe obj)
        {
            _context.Recipes.Update(obj);
        }

        public Recipe? GetById(int id)
        {
            return _context.Recipes
                .Include(r => r.AppUser)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Reviews)
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Recipe> GetAll()
        {
            return _context.Recipes
                .Include(r => r.AppUser)
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Reviews)
                .ToList();
        }

        public List<AppUser> GetAppUsers()
        {
            return _context.AppUsers
                .OrderBy(u => u.UserName)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
