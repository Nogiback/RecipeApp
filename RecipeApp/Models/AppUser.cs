namespace RecipeApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PantryItem> PantryItems { get; set; } = new List<PantryItem>();
    }
}