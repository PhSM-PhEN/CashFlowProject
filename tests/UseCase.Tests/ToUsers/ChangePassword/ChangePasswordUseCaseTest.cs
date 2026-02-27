using CashFlow.Application.UseCases.ToUser.ChangePassword;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using Shouldly;

namespace UseCase.Tests.ToUsers.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Sucess()
        {
            var user = UserBuilder.Build();
            var request = RequestChangePasswordJsonBuilder.Build();
            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);
            await act.ShouldNotThrowAsync();
        }
        [Fact]
        public async Task Error_newPassoword_Empty()
        {
            var user = UserBuilder.Build();
            var request = RequestChangePasswordJsonBuilder.Build();
            request.NewPassword = string.Empty;

            var useCase = CreateUseCase(user, request.Password);

            var act = async () => { await useCase.Execute(request); };
            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ShouldBe(ResourceErrorMessages.INVALID_PASSWORD);
        }
        [Fact]
        public async Task Error_Current_Password_Different()
        {
            var user = UserBuilder.Build();
           
            var request = RequestChangePasswordJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            var act = async () => { await useCase.Execute(request); };
            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ShouldBe(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);

        }
        private static ChangePasswordUseCase CreateUseCase(User user, string? password = null)
        {
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userUpdate = new UserUpdateOnlyRepositoryBuilder().Build(user);


            var loggedUser = LoggedUserBuilder.Build(user);
            var passwordEncripter = new PasswordEcrypterBuilder().Verify(password).Build();

            return new ChangePasswordUseCase(loggedUser, userUpdate, unitOfWork, passwordEncripter);
        }
    }
}
