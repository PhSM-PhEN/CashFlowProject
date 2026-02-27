using CashFlow.Communication.Request.ToUser;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.ToUser;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.ToUser.Update
{
    public class UpdateProfileUseCase(ILoggedUser loggedUser, IUserReadOnlyRespository userReadOnly,
        IUserUpdateOnlyRepository userUpdateOnly, IUnitOfWork unitOfWork) : IUpdateProfileUseCase
    {

        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUserReadOnlyRespository _userReadOnly = userReadOnly;
        private readonly IUserUpdateOnlyRepository _userUpdateOnly = userUpdateOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.Get();
            await Validate(request, loggedUser.Email);

            var user = await _userUpdateOnly.GetById(loggedUser.Id);

            user.Name = request.Name;
            user.Email = request.Email;

            _userUpdateOnly.Update(user);

            await _unitOfWork.Commit();
            
        }
        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result = validator.Validate(request);
            if (currentEmail.Equals(request.Email) == false)
            {
                
                    var userExist = await _userReadOnly.ExistByEmail(request.Email);
                    if (userExist)
                    {
                        result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
                    }
                
                if (result.IsValid == false)
                {
                    var errorMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

                    throw new ErrorOnValidationException(errorMessage);
                }
            }
        }
    }
}
