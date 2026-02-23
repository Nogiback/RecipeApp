using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IRecipeRepository
    {
        void Add(Recipe obj);
        void Delete(int id);
        void Update(Recipe obj);
        Recipe? GetById(int id);
        List<Recipe> GetAll();
        List<AppUser> GetAppUsers();
        void Save();
    }
}
