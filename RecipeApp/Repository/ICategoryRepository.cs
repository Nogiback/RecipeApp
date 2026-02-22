using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface ICategoryRepository
    {
        // CRUD operations for Category
        void Add(Category obj);
        void Delete(int id);
        void Update(Category obj);
        Category GetById(int id);
        List<Category> GetAll();
        void Save();
    }
}