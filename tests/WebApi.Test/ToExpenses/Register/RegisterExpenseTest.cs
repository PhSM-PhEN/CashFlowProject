using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToExpenses.Register
{
    public class RegisterExpenseTest : CashflowClassFixture
    {

        private const string METHOD = "api/expenses";
        private readonly string _token;

        public RegisterExpenseTest(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _token = customWebApplication.USER_MEMBER_TIME.GetToken();
        }

        [Fact]

        public async Task Success()
        {

            var request = RequestExpenseJsonBuilder.Build();
            

            var result = await DoPost(requestUri: METHOD,request: request, token: _token);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("title").GetString().ShouldBe(request.Title);

        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Title_Empty(string culture)
        {
            var request = RequestExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            var result = await DoPost(requestUri: METHOD, request: request, cultureInfo:culture, token: _token);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TITLE_IS_REQUIRED", new System.Globalization.CultureInfo(culture));

            errors.ShouldContain(x => x.GetString()!.Equals(expectedMessage));

        }

    }
}
