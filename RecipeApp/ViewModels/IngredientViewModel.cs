using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingredient name is required")]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Default unit is required")]
        [Display(Name = "Default Unit")]
        public string DefaultUnit { get; set; } = string.Empty;

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int RecipeCount { get; set; }
        public int PantryItemCount { get; set; }

        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
