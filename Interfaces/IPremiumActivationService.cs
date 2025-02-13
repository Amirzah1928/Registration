namespace registration.Interfaces
{
    public interface IPremiumActivationService
    {
        public bool Active(int Plan, string username);
        
    }
}
