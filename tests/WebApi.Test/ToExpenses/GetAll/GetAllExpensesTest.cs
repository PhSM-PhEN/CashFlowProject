using Shouldly;
using System.Text.Json;

namespace WebApi.Test.ToExpenses.GetAll
{
    public class GetAllExpensesTest : CashflowClassFixture
    {
        private const string METHOD = "api/expenses";
        private readonly string _token;

        public GetAllExpensesTest(CustomWebApplicationFactory customWeb) : base(customWeb)
        {
            _token = customWeb.USER_MEMBER_TIME.GetToken();

        }
        [Fact]
        public async Task Success()
        {
            var result = await DoGet(requestUri: METHOD, token: _token);
            result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var expectedResponse = response.RootElement.GetProperty("expenses");
            expectedResponse.EnumerateArray().ShouldNotBeEmpty();
        }
    }
}
