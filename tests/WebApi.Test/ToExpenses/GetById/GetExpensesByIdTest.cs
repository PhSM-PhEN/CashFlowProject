using CashFlow.Communication.Enums;
using CashFlow.Exception.ExceptionBase;
using DocumentFormat.OpenXml.Bibliography;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToExpenses.GetById
{
    public class GetExpensesByIdTest : CashflowClassFixture
    {
        private const string METHOD = "api/expenses";
        private readonly string _token;
        private readonly long _expenseId;
        public GetExpensesByIdTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
            _expenseId = webApplicationFactory.EXPENSE_MEMBER_TIME.GetById();
        }
        [Fact]
        public async Task Success()
        {
            var result = await DoGet(requestUri: $"{METHOD}/{_expenseId}", token: _token);

            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("id").GetInt64().ShouldBe(_expenseId);
            response.RootElement.GetProperty("title").ToString().ShouldNotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("description").ToString().ShouldNotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("date").GetDateTime().ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            response.RootElement.GetProperty("amount").GetDecimal().ShouldBeGreaterThan(0);
            response.RootElement.GetProperty("tags").EnumerateArray().ShouldNotBeEmpty();
            var paymente = response.RootElement.GetProperty("paymentType").GetInt32();
            Enum.IsDefined(typeof(PaymentTypeEnum), paymente).ShouldBeTrue();





        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_No_Found(string culture)
        {
            var result = await DoGet(requestUri: $"{METHOD}/{1000}", token: _token, cultureInfo: culture);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var error = response.RootElement.GetProperty("errorMessages");

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));
            error.EnumerateArray().Count().ShouldBe(1);
            error.EnumerateArray().First().GetString().ShouldBe(expectedMessage);
        }
    }
}
