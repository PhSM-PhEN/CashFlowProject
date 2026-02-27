using AutoMapper;
using CashFlow.Communication.Responses.ToUser;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UseCases.ToUser.GetUser
{
    public class GetUserProfileUseCase(ILoggedUser loggedUser, IMapper mapper) : IGetUserUseProfileCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IMapper _mapper = mapper;


        public async Task<ResponseUserProfile> Execute()
        {
            var user = await _loggedUser.Get();

            return  _mapper.Map<ResponseUserProfile>(user);
        }
    }
}
