using CashFlow.Communication.Request.ToUser;
using CashFlow.Communication.Responses.ToUser;

namespace CashFlow.Application.UseCases.ToUser.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
