using CashFlow.Communication.Request.ToLogin;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;
using System.Globalization;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.ToUsers.ChangePassword
{
    public class ChangePasswordTest : CashflowClassFixture
    {
        private const string METHOD = "api/user/passwordchang";
        private readonly string _token;
        private readonly string _password;
        private readonly string _email;

        public ChangePasswordTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
        {
            _token = webApplicationFactory.USER_MEMBER_TIME.GetToken();
            _password = webApplicationFactory.USER_MEMBER_TIME.GetPassword();
            _email = webApplicationFactory.USER_MEMBER_TIME.GetEmail();
        }
        [Fact]
        public async Task Success()
        {
            var request = RequestChangePasswordJsonBuilder.Build();
            request.Password = _password;

            var response = await DoPut(METHOD, request: request,cultureInfo: "en", token: _token);
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);


            var loginRequest = new RequestLoginJson
            {
                Email = _email,
                Password = _password,
            };


            response = await DoPost("api/login", loginRequest);
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);

            loginRequest.Password = request.NewPassword;

            response = await DoPost("api/login", loginRequest);
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        }
        [Theory]
        [ClassData(typeof(CultureInfoInLineData))]
        public async Task Error_Password_Different(string culture)
        {
            var request = RequestChangePasswordJsonBuilder.Build();

            var response = await DoPut(METHOD, request, token: _token, cultureInfo: culture);

            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

            await using var responsyBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responsyBody);

            var erros = responseData.RootElement.GetProperty("errorMessages");

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

            erros.GetArrayLength().ShouldBe(1);
            erros.EnumerateArray().First().GetString().ShouldBe(expectedMessage);

        }

    }
}
