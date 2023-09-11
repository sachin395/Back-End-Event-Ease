namespace LoginAPI.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } = "User";
        //public byte[]? Image { get; set; }
        public bool IsEmailVerified { get; set; } = false;
    }
}
