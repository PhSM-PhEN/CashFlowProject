using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expense
{

    public interface IExpensesWriteOnlyRespository
    {
        Task Add(Expenses expense);

        Task Delete(long id);
    }

}
