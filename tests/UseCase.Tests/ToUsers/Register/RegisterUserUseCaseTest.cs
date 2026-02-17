using AutoMapper;
using CashFlow.Application.UseCases.ToUser.Register;
using CashFlow.Communication.Request.ToUser;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Criptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCase.Tests.ToUsers.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
            result.Token.ShouldNotBeNullOrEmpty();

        }
        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            
            var useCase = CreateUseCase();


            var act  =async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().ShouldContain(ResourceErrorMessages.NAME_EMPTY);


        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
           
            var useCase = CreateUseCase(request.Email);

            var act  =async () => await useCase.Execute(request);
            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().ShouldContain(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED);

            
        }

        private static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MappperBuilder.Build();
            var unitofwork = UnitOfWorkBuilder.Build();
            var writeRespository = UserWriteOnlyRepositoryBuilder.Build();
            var passwordEncripter = new PasswordEcrypterBuilder().Build();
            var token = JwtTokenGeneretorBuilder.Build();
            var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();

            if (string.IsNullOrEmpty(email) == false)
            {
                userReadOnlyRepository.ExistByEmail(email);
            }
             

            return new RegisterUserUseCase(mapper, passwordEncripter, userReadOnlyRepository.Build(), writeRespository, unitofwork, token);
        }
    }
}
