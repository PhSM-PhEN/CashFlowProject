using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Token;
using CashFlow.Infrastructure.Security.Token;
using Moq;

namespace CommonTestUtilities.Token
{
    public class JwtTokenGeneretorBuilder
    {
       public static IAccessTokenGenerator Build()
        {
            var mock = new Mock<IAccessTokenGenerator>();

            mock.Setup(config => config.GenerateToken(It.IsAny<User>())).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
                "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0." +
                "KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");

            return mock.Object;
        }
    }
}
