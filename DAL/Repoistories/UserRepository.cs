using System.Linq;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly RecipesDB _context;

        public UserRepository(RecipesDB context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Mail == email);
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Mail == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}