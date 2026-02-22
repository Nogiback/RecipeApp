using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CreateIngredientViewModel
    {
        [Required(ErrorMessage = "Ingredient name is required")]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Default unit is required")]
        [Display(Name = "Default Unit (e.g., g, ml, pcs)")]
        public string DefaultUnit { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // This fulfills the dropdown list requirement 
        public IEnumerable<SelectListItem> CategoryOptions { get; set; }
    }
}
