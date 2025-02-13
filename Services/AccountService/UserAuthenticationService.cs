using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using registration.Entities;
using registration.Interfaces;
using System.Security.Claims;

namespace registration.Services.AccountService
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserLoginService _userLoginService;

        public UserAuthenticationService(IHttpContextAccessor httpContextAccessor, UserLoginService userLoginService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userLoginService = userLoginService;
        }


        public async Task<bool> SignInAsync(string username, string password)
        {
            User user = _userLoginService.Validateuser(username, password);

            if (user == null)
                return false;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("IsPremium",user.IsPremium.ToString()),
            new Claim("UserType",user.UserTypes.ToString())
        };


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            return true;

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
