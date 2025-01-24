using registration.Entities;
using registration.Models;

namespace registration.Interfaces
{
    public interface IUserRegistrationService
    {
        Result RegisterUser(RegistrationViewModel model);
    }
}
