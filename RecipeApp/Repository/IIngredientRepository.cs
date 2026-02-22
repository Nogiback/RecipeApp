using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IIngredientRepository
    {
        void Add(Ingredient obj);
        void Delete(int id);
        void Update(Ingredient obj);
        Ingredient? GetById(int id);
        List<Ingredient> GetAll();
        List<Category> GetCategories();
        void Save();
    }
}
