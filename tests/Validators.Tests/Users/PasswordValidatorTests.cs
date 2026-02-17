using CashFlow.Application.UseCases.ToUser.Register;
using CashFlow.Communication.Request.ToUser;
using FluentValidation;
using Shouldly;

namespace Validators.Tests.Users
{
    public class PasswordValidatorTests
    {
        [Theory]

        [InlineData("123456")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("AAAAAAAAA")]
        [InlineData("Aaaaaaaaa")]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        [InlineData("12345678")]
        public void Error_Password_Invalid(string password)
        {
            var validator = new PasswordValidator<RequestRegisterUserJson>();

            var result = validator
                .IsValid(new ValidationContext<RequestRegisterUserJson>
                (new RequestRegisterUserJson()), password);

            result.ShouldBeFalse();

        }

    }
}
