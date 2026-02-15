using CashFlow.Communication.Responses.ToExpenses;

namespace CashFlow.Application.UseCases.ToExpenses.GetAll
{
    public interface IGetAllExpensesUseCase
    {
        Task<ResponseExpensesJson> Execute();
    }
}
