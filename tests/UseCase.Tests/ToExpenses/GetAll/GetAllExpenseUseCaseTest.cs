using CashFlow.Application.UseCases.ToExpenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace UseCase.Tests.ToExpenses.GetAll
{
    public class GetAllExpenseUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();

            var expense = ExpenseBuilder.Collection(user, 10);

            var userCase = CreateUseCase(user, expense);

            var result = await userCase.Execute();

            result.ShouldNotBeNull();
            result.Expenses.ShouldNotBeNull().ShouldNotBeEmpty();

            result.Expenses.ShouldAllBe(expenses => expenses.Id > 0
            && !string.IsNullOrEmpty(expenses.Title)
            && expenses.Amount > 0);



        }

        private GetAllExpensesUseCase CreateUseCase(User user, List<Expenses> expense)
        {
            var repositori = new ExpensesReadOnlyRepositoryBuidler().GetAll(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetAllExpensesUseCase(repositori, mapper, loggedUser);
        }
    }
}
