using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Communication.Responses.ToExpenses;

namespace CashFlow.Application.UseCases.ToExpenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request);
    }
}
