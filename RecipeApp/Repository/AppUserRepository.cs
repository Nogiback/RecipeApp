using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AppUser obj)
        {
            _context.AppUsers.Add(obj);
        }

        public void Delete(int id)
        {
            var user = _context.AppUsers.Find(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
            }
        }

        public void Update(AppUser obj)
        {
            _context.AppUsers.Update(obj);
        }

        public AppUser GetById(int id)
        {
            return _context.AppUsers
                .Include(u => u.Recipes)
                .Include(u => u.Reviews)
                .Include(u => u.PantryItems)
                .FirstOrDefault(u => u.Id == id);
        }

        public List<AppUser> GetAll()
        {
            return _context.AppUsers
                .Include(u => u.Recipes)
                .Include(u => u.Reviews)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}