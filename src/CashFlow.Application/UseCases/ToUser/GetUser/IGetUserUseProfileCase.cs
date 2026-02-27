using CashFlow.Communication.Responses.ToUser;

namespace CashFlow.Application.UseCases.ToUser.GetUser
{
    public interface IGetUserUseProfileCase
    {
        Task<ResponseUserProfile> Execute();
    }
}
