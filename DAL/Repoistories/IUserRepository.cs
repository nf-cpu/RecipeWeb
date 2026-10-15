namespace DAL
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        User? GetById(int id);
        bool EmailExists(string email);
        void Add(User user);
    }
}