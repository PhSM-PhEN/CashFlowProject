using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.ToExpenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseExpenseJson> Execute(RequestExpenseJson request);
    }
}
