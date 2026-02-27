using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.ToUser;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        public IUserUpdateOnlyRepository Build(User user)
        {
            var mock = new Mock<IUserUpdateOnlyRepository>();

            mock.Setup(respository => respository.GetById(user.Id)).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
