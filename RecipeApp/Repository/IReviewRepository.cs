using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IReviewRepository
    {
        // Mandatory
        IEnumerable<Review> GetAll();
        Review GetById(int id);
        void Add(Review review);
        void Update(Review review);
        void Delete(int id);
        void Save();

        // Specific to Review
        IEnumerable<Review> GetReviewsByRecipeId(int recipeId);
        double GetAverageRatingForRecipeAsync(int recipeId); // Great for UI/UX displays
    }
}
