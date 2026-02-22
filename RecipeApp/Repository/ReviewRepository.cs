using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
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

        public double GetAverageRatingForRecipeAsync(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Review GetById(int id)
        {
            return _context.Reviews
                .Include(r => r.AppUser)
                .Include(r => r.Recipe)
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Review> GetReviewsByRecipeId(int recipeId)
        {
            return _context.Reviews
                .Include(r => r.AppUser) 
                .Where(r => r.RecipeId == recipeId)
                .ToList();
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
