using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IPantryItemRepository
    {
        // Mandatory
        IEnumerable<PantryItem> GetAll();
        PantryItem GetById(int id);
        void Add(PantryItem item);
        void Update(PantryItem item);
        void Delete(int id);
        void Save();

        // Specific to PantryItem
        IEnumerable<PantryItem> GetPantryByUserId(int userId);
        IEnumerable<PantryItem> GetExpiringItems(int userId, int daysUntilExpiry); // Useful for dashboard alerts
    }
}
