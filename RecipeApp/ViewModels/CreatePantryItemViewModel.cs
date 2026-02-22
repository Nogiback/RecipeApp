using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CreatePantryItemViewModel
    {
        [Required(ErrorMessage = "Please type an ingredient name.")]
        [Display(Name = "Ingredient")]
        public string SearchTerm { get; set; } // What the user physically types

        public int? ExistingIngredientId { get; set; } // Hidden: populated by JS if they click a match

        [Required]
        [Range(0.1, 10000)]
        public decimal Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
    }
}
