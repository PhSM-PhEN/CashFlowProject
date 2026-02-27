using CashFlow.Communication.Request.ToUser;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.ToUser;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.ToUser.ChangePassword
{
    public class ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository userUpdate, IUnitOfWork unitOfWork, IPasswordEncripter encripter ) : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUserUpdateOnlyRepository _userUpdateOnly = userUpdate;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter = encripter;
        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.Get();

            Validate(request, loggedUser);

            var user  = await _userUpdateOnly.GetById(loggedUser.Id);

            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

            _userUpdateOnly.Update(user);

            await _unitOfWork.Commit();
            
        }
        private void Validate(RequestChangePasswordJson request, User user)
        {
            var validator = new ChangPasswordValidator();

            var result = validator.Validate(request);

            var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

            if (passwordMatch == false)
            {
                if (passwordMatch == false)
                {
                    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID));
                }
            }
            if (result.IsValid == false)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erros);
            }
        }
    }
}
