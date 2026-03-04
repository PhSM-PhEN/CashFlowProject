using Shouldly;

namespace WebApi.Test.ToUsers.Delete
{
    public class DeleteUserAccountTest : CashflowClassFixture
    {
        private readonly string METHOD = "api/user";
        private readonly string _token;

        public DeleteUserAccountTest(CustomWebApplicationFactory customWeb) : base(customWeb)
        {
            _token = customWeb.USER_MEMBER_TIME.GetToken();
        }
        [Fact]
        public async Task Sucess()
        {
            var result = await DoDelete(METHOD,token: _token);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        }


    }
}
