using CashFlow.Communication.Request.ToLogin;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin
{
    public class DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : IClassFixture<CustomWebApplicationFactory>
    {
        private const string METHOD = "api/Login";
        private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();
        private readonly string _email = webApplicationFactory.USER_MEMBER_TIME.GetEmail();
        private readonly string _name = webApplicationFactory.USER_MEMBER_TIME.GetName();
        private readonly string _password = webApplicationFactory.USER_MEMBER_TIME.GetPassword();

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                Password = _password,
            };

            var response = await _httpClient.PostAsJsonAsync(METHOD, request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var responseBoby = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBoby);

            responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
            responseData.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrWhiteSpace();
        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_login_Invalid(string culture)
        {
            var request = RequestLoginJsonBuilder.Build();
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));

            var response = await _httpClient.PostAsJsonAsync(METHOD, request);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            var responseBoby = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBoby);

            var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedErrorMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID"
                ,new CultureInfo(culture));

            errors.ShouldContain(x => x.GetString() == expectedErrorMessage);

        }
    }
}
