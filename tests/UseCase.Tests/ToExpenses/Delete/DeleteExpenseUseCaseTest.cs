using CashFlow.Application.UseCases.ToExpenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace UseCase.Tests.ToExpenses.Delete
{
    public class DeleteExpenseUseCaseTest
    {

        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();

            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(expense.Id);

            await act.ShouldNotThrowAsync();
        }


        [Fact]
        public async Task Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();

            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000);

            var result = await act.ShouldThrowAsync<NotFoundExcepiton>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND).ToString();

        }

        private DeleteExpensesUseCase CreateUseCase(User user, Expenses expenses = null)
        {
            var repositoryWriteOnly = ExpenseWriteOnlyRepositoryBuild.Build();
            var repositoryReadonly = new ExpensesReadOnlyRepositoryBuidler().GetById(user, expenses).Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new DeleteExpensesUseCase(repositoryWriteOnly, repositoryReadonly, unitOfWork, loggedUser);

        }
    }
}
