using registration.Entities;
using registration.Interfaces;
using System.Net;
namespace registration.Services.AccountService
{
    public class PremiumActivationService : IPremiumActivationService
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IComfirmPasswordService _passwordService;
        

        public PremiumActivationService(ApplicationDbcontext dbcontext, IComfirmPasswordService passwordService)
        {
            _dbcontext = dbcontext;
            _passwordService = passwordService;
        }

        public bool Active(int Plan, string username)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.UserName == username);

                if (user == null)
                    return false;

                user.IsPremium = true;
                user.UserTypes = (UserTypes)Enum.GetValues(typeof(UserTypes)).GetValue(Plan);

                var result = _passwordService.UpdateUser(user);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
