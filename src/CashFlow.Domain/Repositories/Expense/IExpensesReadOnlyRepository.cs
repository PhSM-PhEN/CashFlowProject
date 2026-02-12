namespace CashFlow.Domain.Repositories.Expense
{
    public interface IExpensesReadOnlyRepository
    {
        Task<Entities.Expenses?> GetById(long id);
        Task<List<Entities.Expenses>> GetAll();
        Task<List<Entities.Expenses>> FilterByMonth(DateOnly month);
    }
}
