using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;

        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Ingredient obj)
        {
            _context.Ingredients.Add(obj);
        }

        public void Delete(int id)
        {
            var ingredient = _context.Ingredients.Find(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
            }
        }

        public void Update(Ingredient obj)
        {
            _context.Ingredients.Update(obj);
        }

        public Ingredient? GetById(int id)
        {
            return _context.Ingredients
                .Include(i => i.Category)
                .Include(i => i.RecipeIngredients)
                    .ThenInclude(ri => ri.Recipe)
                .Include(i => i.PantryItems)
                .FirstOrDefault(i => i.Id == id);
        }

        public List<Ingredient> GetAll()
        {
            return _context.Ingredients
                .Include(i => i.Category)
                .Include(i => i.RecipeIngredients)
                .Include(i => i.PantryItems)
                .ToList();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories
                .OrderBy(c => c.Name)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
