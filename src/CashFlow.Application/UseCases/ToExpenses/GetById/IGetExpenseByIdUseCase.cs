using CashFlow.Communication.Responses.ToExpenses;

namespace CashFlow.Application.UseCases.ToExpenses.GetById
{
    public interface IGetExpenseByIdUseCase
    {
        Task<ResponseExpenseJson> Execute(long id);
    }
}
