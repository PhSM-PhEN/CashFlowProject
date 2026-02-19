using AutoMapper;
using CashFlow.Communication.Responses.ToExpenses;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.ToExpenses.GetAll
{
    public class GetAllExpensesUseCase(IExpensesReadOnlyRepository expenseRepository, IMapper mapper,
        ILoggedUser loggedUser) : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _expenseRepository = expenseRepository;
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;



        public async Task<ResponseExpensesJson> Execute()
        {
            var loggedUserId = await _loggedUser.Get();

            var result  = await _expenseRepository.GetAll(loggedUserId);

            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };

        }
    }
}
