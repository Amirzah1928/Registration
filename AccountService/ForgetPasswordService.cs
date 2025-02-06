using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using registration.Entities;
using registration.Interfaces;

namespace registration.AccountService
{
    public class ForgetPasswordService : IComfirmPasswordService
    {
        private readonly ApplicationDbcontext _context;
        private readonly IUserPasswordService _userPasswordService;

        public ForgetPasswordService(ApplicationDbcontext context, IUserPasswordService userPasswordService)
        {
            _context = context;
            _userPasswordService = userPasswordService;
        }


        public User FindUserbyEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            return user;
        }



        public string CodeGenrator()
        {
            var code = Random.Shared.Next(100000, 999999);
            return code.ToString();
        }





        public bool ResetPasswordDb(string password, string email)
        {
            var hashedPassword = _userPasswordService.HashPassword(password);
            var result = FindUserbyEmail(email);
            result.Password = hashedPassword;
            var resetResult = UpdateUser(result);
            return resetResult;
        }

        private bool UpdateUser(User user)
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
