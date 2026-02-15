using AutoMapper;
using CashFlow.Communication.Request.ToUser;
using CashFlow.Communication.Responses.ToUser;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.ToUser.Register
{
    public class RegisterUserUseCase(IMapper mapper, IPasswordEncripter passwordEncripter,
        IUserReadOnlyRespository userRespository, IUserWriteOnlyRespository userWriteOnly, IUnitOfWork unitOfWork,
        IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
    {
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        private readonly IMapper _mapper = mapper;
        private readonly IUserReadOnlyRespository _userRespository = userRespository;
        private readonly IUserWriteOnlyRespository _userWriteOnly = userWriteOnly;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;


        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user =  _mapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();


            await _userWriteOnly.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }
        private  async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new UserValidator();
            var result = validator.Validate(request);

            var emaiExist = await _userRespository.ExistsByEmail(request.Email);


            if (emaiExist) 
            {
                result.Errors.Add(new ValidationFailure(string.Empty ,ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            
            if (result.IsValid == false)
            {
                var erroMessages = result.Errors.Select(erros => erros.ErrorMessage).ToList();

                throw new ErrorOnValidationException(erroMessages);
            }
        }
    }
}
