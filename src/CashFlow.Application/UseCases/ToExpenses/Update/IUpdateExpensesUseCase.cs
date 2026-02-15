using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.ToExpenses.Update
{
    public interface IUpdateExpensesUseCase
    {
        Task Execute(long id, RequestExpenseJson request);
    }
}
