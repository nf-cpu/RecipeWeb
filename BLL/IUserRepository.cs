using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        bool EmailExists(string email);
        void Add(User user);
    }
}
