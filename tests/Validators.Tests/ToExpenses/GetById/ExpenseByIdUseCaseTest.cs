using CashFlow.Application.UseCases.ToExpenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace Validators.Tests.ToExpenses.GetById
{
    public class ExpenseByIdUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);

            var useCase = CreateUseCase(loggedUser, expense);

            var result = await useCase.Execute(expense.Id);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(expense.Id);
            result.Title.ShouldBe(expense.Title);
            result.Description.ShouldBe(expense.Description);
            result.Date.ShouldBe(expense.Date);
            result.Amount.ShouldBe(expense.Amount);
            result.PaymentType.ShouldBe((CashFlow.Communication.Enums.PaymentTypeEnum)expense.PaymentType);

        }
        [Fact]

        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var useCase = CreateUseCase(loggedUser);

            var act = async () => await useCase.Execute(id: 1000);

            var result = await act.ShouldThrowAsync<NotFoundExcepiton>();

            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().First().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND).ToString();

        }
        private GetExpenseByIdUseCase CreateUseCase(User user, Expenses? expense = null)
        {
            var repository = new ExpensesReadOnlyRepositoryBuidler().GetById(user, expense).Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
        }
    }
}
