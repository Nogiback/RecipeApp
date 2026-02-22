using System.ComponentModel.DataAnnotations;

namespace RecipeApp.ViewModels
{
    public class AppUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        [Display(Name = "Joined Date")]
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        // For display purposes
        public int RecipeCount { get; set; }
        public int ReviewCount { get; set; }
        public int PantryItemCount { get; set; }
    }
}