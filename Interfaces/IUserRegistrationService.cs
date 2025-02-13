using registration.Entities;
using registration.Models;

namespace registration.Interfaces
{
    public interface IUserRegistrationService
    {
       public Result RegisterUser(RegistrationViewModel model);
    }
}
