using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class ComfirmPasswordViewModel
    {
        [Required(ErrorMessage = "Email is rquired")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "First name must be between 10 and 50 charechter")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address.")]
        public string Email { get; set; }
    }
}
