using CashFlow.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpenseWriteOnlyRepositoryBuild
    {
        public static IExpensesWriteOnlyRespository Build()
        {
            var mock = new Mock<IExpensesWriteOnlyRespository>();

            return mock.Object;
        }
    }
}
