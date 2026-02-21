using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Instructions { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        [Range(1, 1440, ErrorMessage = "Cook time must be between 1 minute and 24 hours")]
        public int CookTimeMins { get; set; }

        // Foreign Key: Who posted this?
        public int AppUserId { get; set; }

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}