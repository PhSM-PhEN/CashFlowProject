using CashFlow.Application.UseCases.ToExpenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace Validators.Tests.ToExpenses.GetAll
{
    public class GetAllExpensesUseCaseTest
    {
        [Fact]

        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();

            var expense = ExpenseBuilder.Collection(loggedUser);
            var useCase = CreateUseCase(loggedUser, expense);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.Expenses.ShouldNotBeNull().ShouldNotBeEmpty();

            result.Expenses.ShouldAllBe
                (
                    ex => ex.Id > 0
                    && !string.IsNullOrEmpty(ex.Title)
                    && ex.Amount > 0
                );
        }

        private GetAllExpensesUseCase CreateUseCase(User user, List<Expenses> expenses)
        {
            var repository = new ExpensesReadOnlyRepositoryBuidler().GetAll(user, expenses).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetAllExpensesUseCase(repository, mapper, loggedUser);
        }
    }
}
