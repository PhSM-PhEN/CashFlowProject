using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Exception.ExceptionBase;
using FluentValidation;

namespace CashFlow.Application.UseCases.ToExpenses
{
    public class ExpenseValidation : AbstractValidator<RequestExpenseJson>
    {
        public ExpenseValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ResourceErrorMessages.TITLE_IS_REQUIRED);     
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.THE_AMOUNT_MUST_THAN_ZERO);
            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE);
            RuleFor(x => x.PaymentType).IsInEnum().WithMessage( ResourceErrorMessages.PAYMENT_TYPE_IS_INVALID);
            RuleFor(x => x.Tags).ForEach(role =>
            {
                role.IsInEnum().WithMessage(ResourceErrorMessages.TAG_TYPE_IS_NOT_SUPPORTED);
            });

        }
    }
}
