namespace RecipeApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; } // 1-5 stars
        public string Comment { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public int AppUserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
