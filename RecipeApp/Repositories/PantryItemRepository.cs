using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repositories
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
            return _context.PantryItems.FirstOrDefault(
                i => i.Id == id);
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
