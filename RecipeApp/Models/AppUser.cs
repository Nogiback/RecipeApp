using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PantryItem> PantryItems { get; set; } = new List<PantryItem>();
    }
}