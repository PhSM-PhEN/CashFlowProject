using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.ToUser;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.ToUser.Delete
{
    public class DeleteUserAccountUseCase(IUserWriteOnlyRespository userWriteOnly,
        ILoggedUser loggedUser, IUnitOfWork unitOfWork) : IDeleteUserAccountUseCase
    {
        private readonly IUserWriteOnlyRespository _respository = userWriteOnly;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Execute()
        {
            var user = await _loggedUser.Get();

            await _respository.Delete(user);
            await _unitOfWork.Commit();
        }
    }
}
