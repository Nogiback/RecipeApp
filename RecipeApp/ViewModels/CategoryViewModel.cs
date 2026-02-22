using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IconUrl { get; set; } = string.Empty;

        // For display
        public int IngredientCount { get; set; }
    }
}