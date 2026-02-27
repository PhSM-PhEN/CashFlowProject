using CashFlow.Application.UseCases.ToUser.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using Shouldly;

namespace UseCase.Tests.ToUsers.Update
{
    public class UpdateUserUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            await act.ShouldNotThrowAsync();
            user.Name.ShouldBe(request.Name);
            user.Email.ShouldBe(request.Email);

        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var user = UserBuilder.Build();

            var request = RequestUpdateUserJsonBuilder.Build();

            request.Name = string.Empty;

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ToString().ShouldBe(ResourceErrorMessages.NAME_EMPTY);

        }
        [Fact]
        public async Task Error_Email_Already_Exist()
        {
            var user = UserBuilder.Build();
            var request = RequestUpdateUserJsonBuilder.Build();

            var useCase = CreateUseCase(user, request.Email);

            var act = async () => await useCase.Execute(request);
            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ToString().ShouldBe(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED);
        }

        private UpdateProfileUseCase CreateUseCase(User user, string? email = null)
        {
            var unitOfWorck = UnitOfWorkBuilder.Build();
            var updateReadOnly = new UserUpdateOnlyRepositoryBuilder().Build(user);
            var loggedUser = LoggedUserBuilder.Build(user);
            var readOnly = new UserReadOnlyRepositoryBuilder();

            if (string.IsNullOrWhiteSpace(email) == false)
            {
                readOnly.ExistByEmail(email);
            }
            return new UpdateProfileUseCase(loggedUser,  readOnly.Build(), updateReadOnly, unitOfWorck);



        }
    }
}
