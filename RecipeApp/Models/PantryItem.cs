namespace RecipeApp.Models
{
    public class PantryItem
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int IngredientId { get; set; }
        public int AppUserId { get; set; }

        // Navigation properties
        public Ingredient? Ingredient { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
