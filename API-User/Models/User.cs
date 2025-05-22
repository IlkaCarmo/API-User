namespace API_User.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public DateTime CreatedAt { get; set; }

        public User (string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The email cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("The password cannot be null or empty.");
            Name = name;
            Email = email;
            PassWord = password;
        }
    }
}
