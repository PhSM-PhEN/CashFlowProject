using AutoMapper;
using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Communication.Responses.ToExpenses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expense;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;


namespace CashFlow.Application.UseCases.ToExpenses.Register
{
    public class RegisterExpenseUseCase(
        IExpensesWriteOnlyRespository expenseRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser) : IRegisterExpenseUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IExpensesWriteOnlyRespository _expenseRepository = expenseRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
        {

            Validate(request);

            var loogedUser = await _loggedUser.Get();

            var expense = _mapper.Map<Expenses>(request);
            expense.UserId = loogedUser.Id;

            await _expenseRepository.Add(expense);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisterExpenseJson>(expense);


        }
        private static void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidation();
            var validationResult = validator.Validate(request);

            if (validationResult.IsValid == false)
            {
                var errorMessage = validationResult.Errors.Select(erros => erros.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessage);
            }



        }
    }
}
/*
 * a funçao retorna um objeto do tipo ResponseRegisterExpenseJson
 * 
 * dentro de todas as funçoes do caso de uso , deve-se implementar a logica de negocio
 * ela sera responsavel por executar todas as etapas necessarias para registrar uma despesa
 * podendo ter funçoes auxiliares privada para validar dados, interagir com repositorios, etc
 */