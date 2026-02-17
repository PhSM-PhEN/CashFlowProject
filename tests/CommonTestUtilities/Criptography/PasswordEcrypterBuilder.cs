using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Criptography
{
    public class PasswordEcrypterBuilder
    {
        private readonly Mock<IPasswordEncripter> _mock;

        public PasswordEcrypterBuilder()
        {
            _mock = new Mock<IPasswordEncripter>();

            _mock.Setup(passwordEcrypter => passwordEcrypter
            .Encrypt(It.IsAny<string>())).Returns("encryptedPassword11");
        }


        public PasswordEcrypterBuilder Verify(string? password)
        {
            if (string.IsNullOrWhiteSpace(password) == false)
            {
                _mock.Setup(passwordEncrypter => passwordEncrypter.
                    Verify(password, It.IsAny<string>())).Returns(true);
            }

            return this;
        }

        public IPasswordEncripter Build() => _mock.Object;

    }
}
