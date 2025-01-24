namespace registration.Interfaces
{
    public interface IUserAuthenticationService 
    {
        Task<bool> SignInAsync(string username, string password);
        Task<bool> SignOutAsync();
    }
}
