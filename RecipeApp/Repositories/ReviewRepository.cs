using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
        }

        public void Delete(int id)
        {
            Review review = GetById(id);
            _context.Reviews.Remove(review);
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }
    }
}
