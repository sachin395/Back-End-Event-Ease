using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string Password_Hash { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Age { get; set; }
        [Phone]
        [Required]
        public int Phone { get; set; }
        public string Role { get; set; } = "User";
        public Byte[] Image { get; set; }
        public bool IsEmail_verified { get; set; }


    }
}
