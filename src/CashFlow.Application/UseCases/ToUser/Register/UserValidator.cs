using CashFlow.Communication.Request.ToUser;
using CashFlow.Exception.ExceptionBase;
using FluentValidation;

namespace CashFlow.Application.UseCases.ToUser.Register
{
    public class UserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public UserValidator() 
        {
            RuleFor(user => user.Name)
               .NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
                .EmailAddress().WithMessage(ResourceErrorMessages.EMAIL_INVALID);

            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        }
    }
}
