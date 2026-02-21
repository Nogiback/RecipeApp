using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        // Foreign Keys
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }

        // Navigation properties
        public Recipe? Recipe { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
