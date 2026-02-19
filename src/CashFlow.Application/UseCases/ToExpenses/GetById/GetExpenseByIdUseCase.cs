using AutoMapper;
using CashFlow.Communication.Responses.ToExpenses;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.ToExpenses.GetById
{
    public class GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper,
        ILoggedUser loggedUser) : IGetExpenseByIdUseCase
    {
        private readonly IExpensesReadOnlyRepository _repository = repository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseExpenseJson> Execute(long id)
        {
            var loggedUser = await _loggedUser.Get();
            var result = await _repository.GetById(loggedUser ,id);

            if (result is null)
            {
                throw new NotFoundExcepiton(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }
            return _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
