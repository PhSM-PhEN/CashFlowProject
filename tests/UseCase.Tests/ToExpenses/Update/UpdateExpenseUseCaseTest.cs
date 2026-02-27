using CashFlow.Application.UseCases.ToExpenses.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Resquests;
using CommonTestUtilities.ToExpense;
using Shouldly;

namespace UseCase.Tests.ToExpenses.Update
{
    public class UpdateExpenseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expense = ExpenseBuilder.Build(loggedUser);
            var request = RequestExpenseJsonBuilder.Build();


            var useCase = CreateUseCase(loggedUser, expense);
            var act = async () => await useCase.Execute(expense.Id, request);
            await act.ShouldNotThrowAsync();
            

        }
        [Fact]
        public async Task Error_Title_Empty()
        {
            var loggedUser = UserBuilder.Build();

            var expense = ExpenseBuilder.Build(loggedUser);

            var request = RequestExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(loggedUser, expense);

            var act = async () => await useCase.Execute(id: expense.Id, request: request);

            var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().Contains(ResourceErrorMessages.TITLE_IS_REQUIRED);
        }
        [Fact]
        public async Task Error_Expense_Not_Found()
        {
            var loggedUser = UserBuilder.Build();
            var request = RequestExpenseJsonBuilder.Build();

            var useCase = CreateUseCase(loggedUser);
            var act = async () => await useCase.Execute(id: 1000, request: request);
            var result = await act.ShouldThrowAsync<NotFoundExcepiton>();
            result.GetErrors().Count.ShouldBe(1);
            result.GetErrors().Contains(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        private UpdateExpensesUseCase CreateUseCase(User user, Expenses? expense = null)
        {

            var mapper = MapperBuilder.Build();
            var repoitory = new ExpenseUpdateReadOnlyRepositoryBuilder().GetById(user, expense).Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new UpdateExpensesUseCase(mapper, unitOfWork, repoitory, loggedUser);

        }

    }
}
