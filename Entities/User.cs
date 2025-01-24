using Microsoft.EntityFrameworkCore;
using registration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace registration.Entities
{
    [Index(nameof(Email),IsUnique = true)]
    [Index(nameof(Password),IsUnique = true)]
    [Index(nameof(UserName),IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15,ErrorMessage = "First name must be between 3 and 15 charechter")]
        [Required(ErrorMessage ="First name is rquired")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is rquired")]
        [MaxLength(15,ErrorMessage = "Last name must be between 3 and 15 charechter")]
        public string LastName { get; set; }
    
        [Required(ErrorMessage = "Email is rquired")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50,ErrorMessage = "Max 50 charecter allowed")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is rquired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is rquired")]
        [MaxLength(20,ErrorMessage = "Max 20 charecter  allowed")]
        public string UserName { get; set; }

    }
}
