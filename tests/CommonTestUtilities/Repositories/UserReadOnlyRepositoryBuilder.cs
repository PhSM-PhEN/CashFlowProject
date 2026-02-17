
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRespository> _repository;

        public UserReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRespository>();
        }
        public void ExistByEmail(string email)
        {
            _repository.Setup(x => x.ExistByEmail(email)).ReturnsAsync(true);
        }
        public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(userRepository => userRepository.GetUserByEmail(user.Email)).ReturnsAsync(user);
            return this;
        }
        public  IUserReadOnlyRespository Build() => _repository.Object;


    }
}
