using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.ToExpenses.Delete
{
    public interface IDeleteExpensesUseCase
    {
        Task Execute(long id);
    }
}
