using RecipeApp.Models;

namespace RecipeApp.ViewModels
{
    public class RecipeDetailsViewModel
    {
        // 1. The core recipe data
        public Recipe Recipe { get; set; }

        // 2. The list of ingredients (from RecipeIngredient junction table)
        public IEnumerable<RecipeIngredient> Ingredients { get; set; }

        // 3. The community reviews
        public IEnumerable<Review> Reviews { get; set; }

        //// Optional UI/UX helper: The average rating
        //public double AverageRating { get; set; }
    }
}
