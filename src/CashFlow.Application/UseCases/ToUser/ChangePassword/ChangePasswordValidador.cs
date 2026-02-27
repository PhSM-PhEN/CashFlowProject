using CashFlow.Application.UseCases.ToUser.Register;
using CashFlow.Communication.Request.ToUser;
using FluentValidation;

namespace CashFlow.Application.UseCases.ToUser.ChangePassword
{
    public class ChangPasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangPasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        }
    }
}
