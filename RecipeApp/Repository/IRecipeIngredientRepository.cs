using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IRecipeIngredientRepository
    {
        // Mandatory
        IEnumerable<RecipeIngredient> GetAll();
        RecipeIngredient GetById(int id);
        void Add(RecipeIngredient recipeIngredient);
        void Update(RecipeIngredient recipeIngredient);
        void Delete(int id);
        void Save();

        // Specific to RecipeIngredient
        IEnumerable<RecipeIngredient> GetIngredientsByRecipeId(int recipeId);
    }
}
