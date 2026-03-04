
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;


namespace WebApi.Test.ToUsers.Register
{
    public class RegisterUserTest :CashflowClassFixture
    {
        private const string METHOD = "api/User";
        private readonly string _token;

        public RegisterUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var result = await  DoPost(METHOD, request);

            result.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
            response.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();


        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Name_Empty(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var result = await DoPost(METHOD, request, cultureInfo: culture, token: _token);

            result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var erros = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("NAME_EMPTY", 
                new System.Globalization.CultureInfo(culture));

            erros.ShouldHaveSingleItem();
            erros.ShouldContain(e => e.GetString()!.Equals(expectedMessage));
        }
    }
}
