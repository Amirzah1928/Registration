using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using registration.Entities;
using registration.Interfaces;
using System.Security.Claims;

namespace registration.Services.AccountServices.UserLoginService
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieCreator _cookiecreator;
        private readonly LoginValidationService _userLoginService;

        public UserAuthenticationService(IHttpContextAccessor httpContextAccessor, LoginValidationService userLoginService, CookieCreator cookieCreator)
        {
            _httpContextAccessor = httpContextAccessor;
            _userLoginService = userLoginService;
            _cookiecreator = cookieCreator;
        }


        public async Task<bool> SignInAsync(string username, string password)
        {
            User user = _userLoginService.Validateuser(username, password);

            if (user == null)
                return false;

           var creationResult = _cookiecreator.CreateCookie(user,username);
           
            return creationResult.Result;

        }









        public async Task<bool> SignOutAsync()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
