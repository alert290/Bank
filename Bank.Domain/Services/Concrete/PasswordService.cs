using Bank.Domain.Services.Abstract;

namespace Bank.Domain.Services.Concrete
{
    public class PasswordService : IPasswordService
    {
        public bool ComparePasswords(string enteredPassword, string hashedStoredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedStoredPassword);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}