using Shouldly;
using System.Globalization;
using System.Net;
using System.Net.Mime;

namespace WebApi.Test.ToExpenses.Report
{
    public class GenerateExpensesReportTest : CashflowClassFixture
    {
        private const string METHOD = "api/report";
        private readonly string _adminToken;
        private readonly string _temMenberToken;
        private readonly DateTime _expenseDate;
        public GenerateExpensesReportTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _adminToken = webApplicationFactory.USER_ADMIN.GetToken();
            _temMenberToken = webApplicationFactory.USER_MEMBER_TIME.GetToken();
            _expenseDate = webApplicationFactory.EXPENSE_ADMIN.GetDate();
        }
        [Fact]
        public async Task Success_Pdf()
        {
            var dateFormate = _expenseDate.ToString("Y", new CultureInfo("en"));

            var result = await DoGet(requestUri: $"{METHOD}/pdf?month={dateFormate:Y}", token: _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            result.Content.Headers.ContentType.ShouldNotBeNull();
            result.Content.Headers.ContentType!.MediaType.ShouldBe(MediaTypeNames.Application.Pdf);
        }
        [Fact]
        public async Task Success_Excel()
        {
            var dateFormate = _expenseDate.ToString("Y", new CultureInfo("en"));

            var result = await DoGet(requestUri: $"{METHOD}/excel?month={dateFormate:Y}", token: _adminToken);
            result.StatusCode.ShouldBe(HttpStatusCode.OK);

            result.Content.Headers.ContentType.ShouldNotBeNull();
            result.Content.Headers.ContentType!.MediaType.ShouldBe(MediaTypeNames.Application.Octet);
        }
        [Fact]
        public async Task Error_Forbidden_User_Not_Allowed_Pdf()
        {
            var result = await DoGet(requestUri: $"{METHOD}/pdf?month={_expenseDate:Y}", token: _temMenberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }
        [Fact]

        public async Task Error_Forbidden_User_Not_Allowed_Excel()
        {
            var result = await DoGet(requestUri: $"{METHOD}/excel?month={_expenseDate:Y}", token: _temMenberToken);
            result.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }
    }
}
