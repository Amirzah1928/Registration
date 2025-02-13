using registration.Entities;
using registration.Interfaces;
namespace registration.Services.AccountServices.UserLoginService
{
    public class LoginValidationService
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IUserPasswordService _passwordService;

        public LoginValidationService(ApplicationDbcontext dbcontext, IUserPasswordService passwordService)
        {
            _dbcontext = dbcontext;
            _passwordService = passwordService;
        }

        public User Validateuser(string username, string password)
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.UserName == username);

            if (user == null || !_passwordService.VerifyPassword(user.Password, password))
                return null;

            return user;

        }
    }
}
