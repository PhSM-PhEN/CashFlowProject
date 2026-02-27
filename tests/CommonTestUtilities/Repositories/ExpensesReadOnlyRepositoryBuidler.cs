using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpensesReadOnlyRepositoryBuidler
    {
        private readonly Mock<IExpensesReadOnlyRepository> _repository;

        public ExpensesReadOnlyRepositoryBuidler()
        {
            _repository = new Mock<IExpensesReadOnlyRepository>();
        }

        public ExpensesReadOnlyRepositoryBuidler GetAll(User user, List<Expenses> expense)
        {
            _repository.Setup(repository => repository.GetAll(user)).ReturnsAsync(expense);
            return this;
        }
        public ExpensesReadOnlyRepositoryBuidler GetById(User user, Expenses expesnse)
        {
            if (expesnse is not null)
            {
                _repository.Setup(repository => repository.GetById(user, expesnse.Id)).ReturnsAsync(expesnse);
            }
            return this;
        }
        public ExpensesReadOnlyRepositoryBuidler FilterByMonth(User user, List<Expenses> expenses)
        {
            _repository.Setup(repository => repository.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);
            return this;
        }
        public IExpensesReadOnlyRepository Build() => _repository.Object;



    }
}
