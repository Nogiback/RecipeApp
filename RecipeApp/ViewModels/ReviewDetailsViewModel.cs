namespace RecipeApp.ViewModels
{
    public class ReviewDetailsViewModel
    {
        public int Id { get; set; }
        public string RecipeTitle { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public int RecipeId { get; set; }
    }
}
