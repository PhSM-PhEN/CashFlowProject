using CashFlow.Application.UseCases.ToExpenses.Register;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using Shouldly;

namespace UseCase.Tests.ToExpenses.Register
{
    public class RegisterExpenseUseCaseTest
    {


            [Fact]
        public async Task Success()
        {

            var user =  UserBuilder.Build();

            var request = RequestExpenseJsonBuilder.Build();

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Title.ShouldBe(request.Title);



        }
        [Fact]
        
        public async Task Error_Title_Empty()
        {
            var user = UserBuilder.Build();

            var request = RequestExpenseJsonBuilder.Build();
            request.Title = string.Empty;
            var useCase = CreateUseCase(user);

            var act = async () => await useCase.Execute(request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

            result.GetErrors().Count.ShouldBe(1);

            result.GetErrors().First().ShouldContain(ResourceErrorMessages.TITLE_IS_REQUIRED);



        }



        private static RegisterExpenseUseCase CreateUseCase(CashFlow.Domain.Entities.User user)
        {
            var repository = ExpenseWriteOnlyRepositoryBuild.Build();
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new RegisterExpenseUseCase(repository, unitOfWork, mapper , loggedUser);
        }
    }
}
