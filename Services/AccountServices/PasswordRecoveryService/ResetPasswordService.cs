using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using registration.Entities;
using registration.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace registration.Services.AccountServices.PasswordRecoveryService
{
    public class ResetPasswordService : IComfirmPasswordService
    {
        private readonly ApplicationDbcontext _context;
        private readonly IUserPasswordService _userPasswordService;

        public ResetPasswordService(ApplicationDbcontext context, IUserPasswordService userPasswordService)
        {
            _context = context;
            _userPasswordService = userPasswordService;
        }



        public bool ResetPasswordDb(string password, string email)
        {
            
            var result = _context.Users.FirstOrDefault(x => x.Email == email);

            if (result == null)
                return false;

            var hashedPassword = _userPasswordService.HashPassword(password);
            result.Password = hashedPassword;


            var resetResult = UpdateUser(result);
            return resetResult;
        }



        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
