using AutoMapper;
using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.ToExpenses.Update
{
    public class UpdateExpensesUseCase(IMapper mapper, IUnitOfWork unitOfWork, 
        IExpensesUpdateOnlyRepository repository,
        ILoggedUser logged) : IUpdateExpensesUseCase
    {
        private readonly ILoggedUser _logged = logged;
        private readonly IMapper _mapper = mapper;
        private readonly IExpensesUpdateOnlyRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(long id, RequestExpenseJson request)
        {

            Validate(request);

            var loggedUser = await _logged.Get();

            var expense = await _repository.GetById(loggedUser ,id);

            if (expense is null)
            {
                throw new NotFoundExcepiton(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            expense.Tags.Clear();

            _mapper.Map(request, expense);

            _repository.Update(expense);

            await _unitOfWork.Commit();
        }
        private static void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidation();

            var resusl = validator.Validate(request);

            if (resusl.IsValid == false)
            {
                var errorMessage = resusl.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessage);
            }
        }
    }
}
