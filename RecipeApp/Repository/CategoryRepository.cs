using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Category obj)
        {
            _context.Categories.Add(obj);
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }

        public Category GetById(int id)
        {
            return _context.Categories
                .Include(c => c.Ingredients)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAll()
        {
            return _context.Categories
                .Include(c => c.Ingredients)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}