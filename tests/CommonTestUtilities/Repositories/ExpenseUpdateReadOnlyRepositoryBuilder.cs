using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpenseUpdateReadOnlyRepositoryBuilder
    {
        private readonly Mock<IExpensesUpdateOnlyRepository> _repository;

        public ExpenseUpdateReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IExpensesUpdateOnlyRepository>();
        }

        public ExpenseUpdateReadOnlyRepositoryBuilder GetById(User user, Expenses? expenses)
        {
            if (expenses is not null)
            {
                _repository.Setup(repository => repository.GetById(user, expenses.Id)).ReturnsAsync(expenses);
            }
            return this;

        }
        public IExpensesUpdateOnlyRepository Build() => _repository.Object;
    }

}
