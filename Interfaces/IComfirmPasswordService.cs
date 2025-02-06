using registration.Entities;

namespace registration.Interfaces
{
    public interface IComfirmPasswordService
    {
        public User FindUserbyEmail(string email);
        public string CodeGenrator();
        public bool ResetPasswordDb(string password, string email);
    }
}
