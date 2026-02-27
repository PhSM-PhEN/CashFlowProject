using CashFlow.Application.UseCases.ToUser.GetUser;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase.Tests.ToUsers.GetProfile
{
    public class GetUserProfileUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var user = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            var result = await useCase.Execute();

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Email.ShouldBe(user.Email);
        }
        private GetUserProfileUseCase CreateUseCase(User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            return new GetUserProfileUseCase(loggedUser, mapper);
        }
    }
}
