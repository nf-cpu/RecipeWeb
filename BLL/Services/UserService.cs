using DAL;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public bool EmailExists(string email)
        {
            return _userRepository.EmailExists(email);
        }

        public void Register(User user)
        {
            // לוגיקה עסקית נוספת כגון בדיקת תקינות או הצפנת סיסמה במידת הצורך
            _userRepository.Add(user);
        }
    }
}