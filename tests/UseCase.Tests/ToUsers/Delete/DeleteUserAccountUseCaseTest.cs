using CashFlow.Application.UseCases.ToUser.Delete;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using Shouldly;

namespace UseCase.Tests.ToUsers.Delete
{
    public class DeleteUserAccountUseCaseTest
    {
        [Fact]
        public async Task Seccess()
        {
            var user = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => { await useCase.Execute(); };

            await act.ShouldNotThrowAsync();
        }
        private DeleteUserAccountUseCase CreateUseCase(User user)
        {
            var repository = UserWriteOnlyRepositoryBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteUserAccountUseCase(repository, loggedUser, unitOfWork);
        }
    }
}
