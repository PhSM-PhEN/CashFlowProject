using CashFlow.Domain.Repositories.Expense;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpenseWriteOnlyRepositoryBuilder
    {
        

        public static IExpensesWriteOnlyRespository Build()
        {
            var mock = new Mock<IExpensesWriteOnlyRespository>();

            return mock.Object;
        }
        

        
    }
}
