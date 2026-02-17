using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Criptography
{
    public class PasswordEcripterBuilder
    {
        public static IPasswordEncripter Build()
        {
            var mock = new Mock<IPasswordEncripter>();
            mock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns("encryptedPassword");

            return mock.Object;
        }
    }
}
