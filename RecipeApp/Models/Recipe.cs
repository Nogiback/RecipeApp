namespace RecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int CookTimeMins { get; set; }
        public int AppUserId { get; set; }

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}