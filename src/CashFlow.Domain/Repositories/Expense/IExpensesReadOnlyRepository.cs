namespace CashFlow.Domain.Repositories.Expense
{
    public interface IExpensesReadOnlyRepository
    {
        Task<Entities.Expenses?> GetById(Domain.Entities.User user, long id);
        Task<List<Entities.Expenses>> GetAll(Domain.Entities.User user);
        Task<List<Entities.Expenses>> FilterByMonth(Domain.Entities.User user ,DateOnly month);
    }
}
