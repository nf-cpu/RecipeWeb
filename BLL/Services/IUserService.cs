using DAL;
using DTO;

namespace BLL.Services
{
    public interface IUserService
    {
        User? GetByEmail(string email);
        bool EmailExists(string email);
        void Register(User user);
    }
}