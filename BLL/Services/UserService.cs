using DAL;
using DTO;
using BLL.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly ITokenService _tokens;
        private readonly IPasswordHasher<User> _hasher;

        public UserService(IUserRepository users, ITokenService tokens, IPasswordHasher<User> hasher)
        {
            _users = users;
            _tokens = tokens;
            _hasher = hasher;
        }

        public void Register(RegisterDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();
            if (_users.EmailExists(email))
                throw new ConflictException("Email address is already registered");

            // התפקיד נקבע בשרת, לא מגיע מהלקוח
            var user = new User { Mail = email, Role = "User" };
            user.Password = _hasher.HashPassword(user, dto.Password);

            _users.Add(user);
        }

        public AuthResponseDto? Login(LoginDto dto)
        {
            var email = dto.Email.Trim().ToLowerInvariant();
            var user = _users.GetByEmail(email);
            if (user is null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return _tokens.CreateToken(user);
        }
    }
}