using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToExpenses.Upadate
{
    public class UpdateExpenseTests : CashflowClassFixture
    {
        private const string METHOD = "api/expenses";
        private readonly string _token;
        private readonly long _expenseId;
        public UpdateExpenseTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
            _expenseId = webApplicationFactory.EXPENSE_MEMBER_TIME.GetById();
        }
        [Fact]
        public async Task Success()
        {
            var request = RequestExpenseJsonBuilder.Build();
            var result = await DoPut(requestUri: $"{METHOD}/{_expenseId}", request: request, token: _token);

            result.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Title_Empty(string culture)
        {
            var request = RequestExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            var result = await DoPut(requestUri: $"{METHOD}/{_expenseId}", request: request, token: _token, cultureInfo: culture);
            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var erros = response.RootElement.GetProperty("errorMessages");

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("TITLE_IS_REQUIRED", new CultureInfo(culture));

            erros.EnumerateArray().Count().ShouldBe(1);
            erros.EnumerateArray().First().GetString().ShouldBe(expectedMessage);


        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var request = RequestExpenseJsonBuilder.Build();
            var result = await DoPut(requestUri: $"{METHOD}/1000", request: request, token: _token, cultureInfo: culture);
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var erros = response.RootElement.GetProperty("errorMessages");
            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));
            erros.EnumerateArray().Count().ShouldBe(1);
            erros.EnumerateArray().First().GetString().ShouldBe(expectedMessage);

        }
    }
}
