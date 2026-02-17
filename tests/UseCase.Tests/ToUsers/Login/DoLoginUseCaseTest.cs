using CashFlow.Application.UseCases.ToLogin;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCase.Tests.ToUsers.Login
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestLoginJsonBuilder.Build();
           
            var user = UserBuilder.Build();

            user.Email = request.Email;

            var useCase = CreateUseCase(user, request.Password);    

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Token.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Error_User_Not_Found()
        {
            var request = RequestLoginJsonBuilder.Build();

            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user, request.Password);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<InvalidLoginException>();
            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);

        }



        [Fact]
        public async Task Error_Password_Not_Math()
        {
            var request = RequestLoginJsonBuilder.Build();
            var user = UserBuilder.Build();

            request.Email = user.Email;

            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<InvalidLoginException>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID);


        }


        private static DoLoginUseCase CreateUseCase( User user, string? password = null)
        {

            var passwordEncripter = new PasswordEcrypterBuilder().Verify(password).Build();
            var token = JwtTokenGeneretorBuilder.Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

            

            return new DoLoginUseCase(userReadOnlyRepository , passwordEncripter , token);


        }
    }
}
