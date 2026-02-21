using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 stars

        [Required]
        public string Comment { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.Now;

        // Foreign Keys
        public int AppUserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
