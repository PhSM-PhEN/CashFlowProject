using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expense
{
    public interface IExpensesUpdateOnlyRepository
    {
        void Update(Expenses expense);
        Task<Expenses?> GetById(long id);
    }
}
