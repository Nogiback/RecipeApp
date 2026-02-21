using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public interface IAppUserRepository
    {
        // CRUD operations for AppUser
        void Add(AppUser obj);
        void Delete(int id);
        void Update(AppUser obj);
        AppUser GetById(int id);
        List<AppUser> GetAll();
        void Save();
    }
}