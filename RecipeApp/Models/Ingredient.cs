namespace RecipeApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string DefaultUnit { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
        public ICollection<PantryItem> PantryItems { get; set; } = new List<PantryItem>();
    }
}
