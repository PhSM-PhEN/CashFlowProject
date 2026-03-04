using Shouldly;
using System.Text.Json;

namespace WebApi.Test.ToUsers.GetProfile
{
    public class GetUserProfileTest : CashflowClassFixture
    {
        private const string METHOD = "api/user";

        private readonly string _token;
        private readonly string _userName;
        private readonly string _userEmail;
        public GetUserProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
            _userName = webApplicationFactory.USER_MEMBER_TIME.GetName();
            _userEmail = webApplicationFactory.USER_MEMBER_TIME.GetEmail();
        }
        [Fact]
        public async Task Success()
        {
            var result = await DoGet(METHOD, token: _token);

            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            var body = await result.Content.ReadAsStreamAsync();
            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").ToString().ShouldBe(_userName);
            response.RootElement.GetProperty("email").ToString().ShouldBe(_userEmail);
        }
    }
}
