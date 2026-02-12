using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.ToExpenses.GetAll
{
    public interface IGetAllExpensesUseCase
    {
        Task<ResponseExpensesJson> Execute();
    }
}
