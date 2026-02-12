using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.ToExpenses.GetById
{
    public interface IGetExpenseByIdUseCase
    {
        Task<ResponseExpenseJson> Execute(long id);
    }
}
