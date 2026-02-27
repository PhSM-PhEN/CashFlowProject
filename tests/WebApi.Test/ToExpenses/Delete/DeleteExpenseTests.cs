using CashFlow.Exception.ExceptionBase;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToExpenses.Delete
{
    public class DeleteExpenseTests : CashflowClassFixture
    {
        private const string METHOD = "api/expenses";
        private readonly string _token;
        private readonly long _expenseId;
        public DeleteExpenseTests(CustomWebApplicationFactory customWebApplication) : base(customWebApplication)
        {
            _token = customWebApplication.USER_MEMBER_TIME.GetToken();
            _expenseId = customWebApplication.EXPENSE_MEMBER_TIME.GetById();
        }
        [Fact]
        public async Task Success()
        {
            var result = await DoDelete(requestUri: $"{METHOD}/{_expenseId}", token: _token);
            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            result = await DoGet(requestUri: $"{METHOD}/{_expenseId}", token: _token);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var result = await DoDelete(requestUri: $"{METHOD}/1000", token: _token, cultureInfo: culture);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();
            var response = JsonDocument.Parse(body);

            var error = response.RootElement.GetProperty("errorMessages");

            error.EnumerateArray().Count().ShouldBe(1);
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));
        }
    }
}
