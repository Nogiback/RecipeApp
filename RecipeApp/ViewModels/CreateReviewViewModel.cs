using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class CreateReviewViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeTitle { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "Your Comment")]
        public string Comment { get; set; } = string.Empty;

        public int SelectedAppUserId { get; set; }
        public IEnumerable<AppUser> Users { get; set; } = new List<AppUser>();


    }
}
