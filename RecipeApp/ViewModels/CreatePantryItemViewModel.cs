using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CreatePantryItemViewModel
    {
        public int Id { get; set; }

        [ValidateNever]
        public int UserId { get; set; }

        [Display(Name = "Ingredient")]
        public int? ExistingIngredientId { get; set; } // Hidden: populated by JS if they click a match

        [Required]
        [Range(0.1, 10000)]
        public decimal Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> IngredientOptions { get; set; }
    }
}
