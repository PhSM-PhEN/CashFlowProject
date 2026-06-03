using CashFlow.Communication.Request.ToExpenses;

namespace CashFlow.Application.UseCases.ToExpenses.Update
{
    public interface IUpdateExpensesUseCase
    {
        Task Execute(long id, RequestExpenseJson request);
    }
}
