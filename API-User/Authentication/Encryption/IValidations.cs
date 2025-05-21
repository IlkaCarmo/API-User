namespace API_User.Authentication.Encryption
{
    public interface IValidations
    {
         string HashPassword(string password);
         bool VerifyPassword(string password, string hashedPassword);
    }
}
