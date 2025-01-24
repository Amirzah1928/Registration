using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class RegistrationViewModel
    {
        [StringLength(20, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 20 charechter")]
        [Required(ErrorMessage = "First name is rquired")]
        [RegularExpression(@"^([\u0600-\u06FF]+( [\u0600-\u06FF]+)*)|([a-zA-Z]+( [a-zA-Z]+)*)$",
        ErrorMessage = "Invalid format.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is rquired")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 30 charechter")]
        [RegularExpression(@"^([\u0600-\u06FF]+( [\u0600-\u06FF]+)*)|([a-zA-Z]+( [a-zA-Z]+)*)$",
        ErrorMessage = "Invalid format.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is rquired")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "First name must be between 10 and 50 charechter")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Username is rquired")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 15 charechter")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Only English letters and numbers are allowed.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 charechter")]
        [RegularExpression(@"^(?=.*\d)[a-zA-Z\d]+$",
        ErrorMessage = "Password must contain only numbers or a combination of letters and numbers.")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please comfirm your password")]
        public string ComfirmPassword { get; set; }


    }
}
