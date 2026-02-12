using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expense;

namespace CashFlow.Application.UseCases.ToExpenses.GetAll
{
    public class GetAllExpensesUseCase(IExpensesReadOnlyRepository expenseRepository, IMapper mapper) : IGetAllExpensesUseCase
    {
        private readonly IExpensesReadOnlyRepository _expenseRepository = expenseRepository;
        private readonly IMapper _mapper = mapper;



        public async Task<ResponseExpensesJson> Execute()
        {
            var result  = await _expenseRepository.GetAll();

            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };

        }
    }
}
