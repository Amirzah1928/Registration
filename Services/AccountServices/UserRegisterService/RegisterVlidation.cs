using registration.Entities;
using registration.Interfaces;

namespace registration.Services.AccountService.UserRegisterService
{
    public class RegisterVlidation
    {
        private readonly ApplicationDbcontext _dbContext;

        public RegisterVlidation(ApplicationDbcontext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public Result Validate(string username, string password, string email)
        {
            if (password == username)
                return new Result { Success = false, Message = "Password cannot be the same as the username." };

            if (_dbContext.Users.Any(u => u.UserName == username))
                return new Result { Success = false, Message = "Username is already taken." };

            if (_dbContext.Users.Any(u => u.Email == email))
                return new Result { Success = false, Message = "Email is already registered." };
            
            
            return new Result { Success = true };
        }
    }
}
