using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public class PantryItemRepository : IPantryItemRepository
    {
        private readonly AppDbContext _context;

        public PantryItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(PantryItem item)
        {
            _context.PantryItems.Add(item);
        }

        public void Delete(int id)
        {
            PantryItem itemToDelete = GetById(id);
            _context.PantryItems.Remove(itemToDelete);
        }

        public IEnumerable<PantryItem> GetAll()
        {
            return _context.PantryItems.ToList();
        }

        public PantryItem GetById(int id)
        {
            return _context.PantryItems
                .Include(p => p.Ingredient) // Eager load the Ingredient to avoid null reference issues in the view
                .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<PantryItem> GetExpiringItems(int userId, int daysUntilExpiry)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PantryItem> GetPantryByUserId(int userId)
        {
            return _context.PantryItems
                    .Include(p => p.Ingredient)
                    .ThenInclude(i => i.Category)
                    .Where(p => p.AppUserId == userId)
                    .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PantryItem item)
        {
            _context.Update(item);
        }
    }
}
