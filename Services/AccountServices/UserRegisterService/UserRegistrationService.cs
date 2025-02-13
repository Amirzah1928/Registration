using Microsoft.EntityFrameworkCore;
using registration.Entities;
using registration.Interfaces;
using registration.Models;

namespace registration.Services.AccountService.UserRegisterService
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly ApplicationDbcontext _dbContext;
        private readonly IUserPasswordService _passwordService;
        private readonly RegisterVlidation _registerVlidation;

        public UserRegistrationService(ApplicationDbcontext dbcontext, IUserPasswordService passwordService, RegisterVlidation registerVlidation)
        {
            _dbContext = dbcontext;
            _passwordService = passwordService;
            _registerVlidation = registerVlidation;
        }

        public Result RegisterUser(RegistrationViewModel model)
        {

            Result validationResult = _registerVlidation.Validate(model.UserName, model.Password, model.Email);
            if (validationResult.Success == false)
                return validationResult;



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
