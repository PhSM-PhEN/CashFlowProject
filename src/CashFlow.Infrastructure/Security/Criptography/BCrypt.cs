using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;
namespace CashFlow.Infrastructure.Security.Criptography
{
    public class BCrypt : IPasswordEncripter
    {
        public string Encrypt(string password)
        {
            string paswwordHash = BC.HashPassword(password);

           

            return paswwordHash;
        }
        public bool Verify(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }

}
