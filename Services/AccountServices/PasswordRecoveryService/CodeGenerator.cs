namespace registration.Services.AccountServices.PasswordRecoveryService
{
    public class CodeGenerator
    {
        public string CodeGenrator()
        {
            var code = Random.Shared.Next(100000, 999999);
            return code.ToString();
        }
    }
}
