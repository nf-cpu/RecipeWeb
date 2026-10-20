using DAL;
using DTO;

namespace BLL.Services
{
    public interface IUserService
    {
        User? GetByEmail(string email);
        bool EmailExists(string email);
        bool Register(RegisterDto dto);
        AuthResponseDto? Login(LoginDto dto);
    }
}