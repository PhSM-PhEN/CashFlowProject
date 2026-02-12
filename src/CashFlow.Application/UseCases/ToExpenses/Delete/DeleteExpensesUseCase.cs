
using AutoMapper;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.ToExpenses.Delete
{
    public class DeleteExpensesUseCase(IExpensesWriteOnlyRespository respository, IUnitOfWork unitOfWork ) : IDeleteExpensesUseCase 
    {
        private readonly IExpensesWriteOnlyRespository _respository = respository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task Execute(long id)
        {
            var result = await _respository.Delete(id);

            if(result == false)
            {
                throw new NotFoundExcepiton(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            await _unitOfWork.Commit();
        }
    }
}
