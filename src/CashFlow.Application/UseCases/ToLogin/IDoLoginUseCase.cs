using CashFlow.Communication.Request.ToLogin;
using CashFlow.Communication.Responses.ToUser;

namespace CashFlow.Application.UseCases.ToLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
    }
}
