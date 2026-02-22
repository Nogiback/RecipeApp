using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Models
{
    public class PantryItem
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; } = DateTime.Now;

        // Foreign Keys
        public int IngredientId { get; set; }
        public int AppUserId { get; set; }

        // Navigation properties
        public Ingredient? Ingredient { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
