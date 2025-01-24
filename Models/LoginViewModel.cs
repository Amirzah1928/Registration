using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Max 20 charecter  allowed")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is rquired")]
        [MaxLength(20, ErrorMessage = "Max 20 charecter  allowed")]
        public string UserName { get; set; }
    }
}
