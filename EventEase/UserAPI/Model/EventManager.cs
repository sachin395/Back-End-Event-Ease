namespace UserAPI.Model
{
    public class EventManager
    {
        public int ManagerId { get; set; }
        public string Organization_Name { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public byte[] Image { get; set; }
    }
}

