using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        [DisplayName("NewPassword")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 charechter")]
        [RegularExpression(@"^(?=.*\d)[a-zA-Z\d]+$",
        ErrorMessage = "Password must contain only numbers or a combination of letters and numbers.")]
        public string Password { get; set; }

        [DisplayName("ComfirmNewPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please comfirm your password")]
        public string ComfirmPassword { get; set; }
    }
}
