using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using registration.Entities;
using System.Security.Claims;

namespace registration.Services.AccountServices.UserLoginService
{
    public class CookieCreator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieCreator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> CreateCookie(User user, string username)
        {
            try
            {
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

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
