using CashFlow.Domain.Repositories.ToUser;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRespository Build()
        {
            var mock = new Mock<IUserWriteOnlyRespository>();
            return mock.Object;
        }
    }
}
