using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
