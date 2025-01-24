using registration.Entities;
using registration.Interfaces;
namespace registration.AccountService
{
    public class UserLoginService
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IUserPasswordService _passwordService;

        public UserLoginService(ApplicationDbcontext dbcontext, IUserPasswordService passwordService)
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
