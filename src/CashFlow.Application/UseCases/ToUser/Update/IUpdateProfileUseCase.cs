using CashFlow.Communication.Request.ToUser;

namespace CashFlow.Application.UseCases.ToUser.Update
{
    public  interface IUpdateProfileUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
