using CashFlow.Application.UseCases.ToExpenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace UseCase.Tests.ToExpenses.GetById
{
    public class ExpenseByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase =  CreateUseCase(loggedUser, expense);
            var result = await useCase.Execute(expense.Id);

            result.ShouldNotBeNull();

        }
        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000);

            var result = await act.ShouldThrowAsync<NotFoundExcepiton>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().ShouldContain(ResourceErrorMessages.EXPENSE_NOT_FOUND);


        }

        private static GetExpenseByIdUseCase CreateUseCase(User user, Expenses? expenses = null)
        {
            var repository = new ExpensesReadOnlyRepositoryBuidler().GetById(user, expenses).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
        }

    }
}
