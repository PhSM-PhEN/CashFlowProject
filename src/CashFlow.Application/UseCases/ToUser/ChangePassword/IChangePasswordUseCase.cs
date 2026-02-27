using CashFlow.Communication.Request.ToUser;

namespace CashFlow.Application.UseCases.ToUser.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
