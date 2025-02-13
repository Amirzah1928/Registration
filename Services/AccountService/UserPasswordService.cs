using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;
using registration.Interfaces;

namespace registration.Services.AccountService
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
