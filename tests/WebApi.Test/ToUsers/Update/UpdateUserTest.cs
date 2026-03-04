using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToUsers.Update
{
    public class UpdateUserTest : CashflowClassFixture
    {
        private const string METHOD = "api/user";
        private readonly string _token;
        public UpdateUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
        }
        [Fact]
        public async Task Sucess()
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            var response = await DoPut(METHOD, request, token: _token);

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Name_Empty(string culture)
        {
            var request = RequestUpdateUserJsonBuilder.Build();
            request.Name = string.Empty;

            var response = await DoPut(METHOD, request,  culture, token: _token);
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errorMessages");

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));
            errors.GetArrayLength().ShouldBe(1);
            errors.EnumerateArray().First().GetString().ShouldBe(expectedMessage);
        }
    }
}
