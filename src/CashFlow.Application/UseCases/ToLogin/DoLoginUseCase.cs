using CashFlow.Communication.Request.ToLogin;
using CashFlow.Communication.Responses.ToUser;
using CashFlow.Domain.Repositories.ToUser;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Token;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.ToLogin
{
    public class DoLoginUseCase(IUserReadOnlyRespository userRepository,
        IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRespository _userRepository = userRepository;
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;


        public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
        {


            var user = await _userRepository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();


            var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

            if(passwordMatch == false)
            {
                throw new InvalidLoginException();
            }

            return new ResponseRegisterUserJson()
            {
                Name = user!.Name,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }
    }
}
