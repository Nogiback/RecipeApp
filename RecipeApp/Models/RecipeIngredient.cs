namespace RecipeApp.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        // Navigation properties
        public Recipe? Recipe { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
