using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class PremiumPlansViewModel
    {

        [Required(ErrorMessage = "Username is rquired")]
        [MaxLength(20, ErrorMessage = "Max 20 charecter  allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PaymentCode is rquired")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "PaymentCode must be between 3 and 30 charechter")]
        public string PaymentCode { get; set; } // payment code is symbol of payment oppreation
    }
}
