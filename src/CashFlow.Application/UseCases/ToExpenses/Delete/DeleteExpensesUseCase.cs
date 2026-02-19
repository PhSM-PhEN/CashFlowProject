using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.ToExpenses.Delete
{
    public class DeleteExpensesUseCase(IExpensesWriteOnlyRespository respository,
        IExpensesReadOnlyRepository expensesReadOnly,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser) : IDeleteExpensesUseCase 
    {
        private readonly IExpensesWriteOnlyRespository _respository = respository;
        private readonly IExpensesReadOnlyRepository _expenseReadOnly = expensesReadOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILoggedUser _loggedUser = loggedUser;


        public async Task Execute(long id)
        {
            var loggedUserId = await _loggedUser.Get();

            var expense = await _expenseReadOnly.GetById(loggedUserId, id);

            if (expense is null)
            {
                
                throw new NotFoundExcepiton(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _respository.Delete(id);

            await _unitOfWork.Commit();
        }
    }
}
