using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CreateRecipeIngredientViewModel
    {
        public int Id { get; set; }
        // Hidden field to know which recipe we are adding to
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Please select an ingredient.")]
        [Display(Name = "Ingredient")]
        public int SelectedIngredientId { get; set; }

        // The Dropdown options
        [ValidateNever]
        public IEnumerable<SelectListItem> IngredientOptions { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0.01, 10000, ErrorMessage = "Please enter a valid amount.")]
        public decimal Quantity { get; set; }

        // Optional: A display string if the unit is different from the default (e.g., "cups" instead of "grams")
        [Display(Name = "Unit")]
        public string? UnitDisplay { get; set; }
    }
}
