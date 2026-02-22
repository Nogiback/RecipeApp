using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly AppDbContext _context;

        public RecipeIngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(RecipeIngredient recipeIngredient)
        {
            _context.Add(recipeIngredient);
        }

        public void Delete(int id)
        {
            RecipeIngredient? recipeIngredient = GetById(id);
            _context.Remove(recipeIngredient);
        }

        public IEnumerable<RecipeIngredient> GetAll()
        {
            return _context.RecipeIngredients.ToList();
        }

        public RecipeIngredient GetById(int id)
        {
            return _context.RecipeIngredients.FirstOrDefault(
                rep => rep.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(RecipeIngredient recipeIngredient)
        {
            _context.Update(recipeIngredient);
        }
    }
}
