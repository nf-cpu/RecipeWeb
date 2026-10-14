using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserRepository : IUserRepository
    {
        private readonly RecipesDB _context;

        public UserRepository(RecipesDB context) => _context = context;

        public User? GetByEmail(string email) =>
            _context.Users.FirstOrDefault(u => u.Mail == email);

        public bool EmailExists(string email) =>
            _context.Users.Any(u => u.Mail == email);

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
