using Microsoft.EntityFrameworkCore;
using registration.Entities;
using registration.Interfaces;
using registration.Models;

namespace registration.Services.AccountService
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly ApplicationDbcontext _dbContext;
        private readonly IUserPasswordService _passwordService;

        public UserRegistrationService(ApplicationDbcontext dbcontext, IUserPasswordService passwordService)
        {
            _dbContext = dbcontext;
            _passwordService = passwordService;
        }

        public Result RegisterUser(RegistrationViewModel model)
        {
            if (model.Password == model.UserName)
                return new Result { Success = false, Message = "Password cannot be the same as the username." };

            if (_dbContext.Users.Any(u => u.UserName == model.UserName))
                return new Result { Success = false, Message = "Username is already taken." };

            if (_dbContext.Users.Any(u => u.Email == model.Email))
                return new Result { Success = false, Message = "Email is already registered." };


            User user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = _passwordService.HashPassword(model.Password),
            };

            try
            {
                _dbContext.Add(user);
                _dbContext.SaveChanges();

                return new Result { Success = true, Message = $"{model.FirstName} {model.LastName} was registered successfully" };

            }
            catch (Exception ex)
            {
                return new Result { Success = false, Message = "An unexpected error occurred. Please try again." };
            }

        }
    }
}
