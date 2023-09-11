using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models
{
    public class LoginModel
    {
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [PasswordPropertyText]
        [MinLength(8, ErrorMessage = "Length should be more then 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
