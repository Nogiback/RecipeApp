using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Recipe title is required")]
        [Display(Name = "Recipe Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Instructions are required")]
        public string Instructions { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Range(1, 1440, ErrorMessage = "Cook time must be between 1 minute and 24 hours")]
        [Display(Name = "Cook Time (minutes)")]
        public int CookTimeMins { get; set; }

        [Display(Name = "Created By")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a user")]
        public int AppUserId { get; set; }

        public string AppUserName { get; set; } = string.Empty;

        public int IngredientCount { get; set; }
        public int ReviewCount { get; set; }

        public IEnumerable<SelectListItem> AppUserOptions { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<RecipeIngredientItemViewModel> IngredientItems { get; set; } = new List<RecipeIngredientItemViewModel>();
    }

    public class RecipeIngredientItemViewModel
    {
        public string IngredientName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
