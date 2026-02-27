using CashFlow.Application.UseCases.ToExpenses.Report.Excel;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace UseCase.Tests.ToExpenses.Report.Excel
{
    public class GenereteExpenseReportExcelUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Collection(loggedUser);


            var userCase = CreateUseCase(loggedUser, expense);

            var result = await userCase.Execute(DateOnly.FromDateTime(DateTime.Today));
            result.ShouldNotBeNull();
        }
        [Fact]
        public async Task Success_Empty()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Collection(loggedUser);


            var userCase = CreateUseCase(loggedUser, []);

            var result = await userCase.Execute(DateOnly.FromDateTime(DateTime.Today));
            result.ShouldBeEmpty();
        }
        private GenereteExpenseReportExcelUseCase CreateUseCase(User user, List<Expenses> expenses)
        {
            var repository = new ExpensesReadOnlyRepositoryBuidler().FilterByMonth(user, expenses).Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GenereteExpenseReportExcelUseCase(repository, loggedUser);
        }
    }
}
