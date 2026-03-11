using CashFlow.Application.UseCases.ToExpenses;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Resquests;
using Shouldly;

namespace Validators.Tests.ToExpenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Success()
        {
            // Arrange

            var validator = new ExpenseValidation();// Instantiate the validator
            var request = RequestExpenseJsonBuilder.Build();

            // Act

            var result = validator.Validate(request);// Validate the request using the validator

            // Assert
            result.IsValid.ShouldBeTrue();


        }
        [Fact]
        public void Fail_When_Title_Is_Empty()
        {
            // Arrange
            var validator = new ExpenseValidation();// Instantiate the validator
            var request = RequestExpenseJsonBuilder.Build();
            request.Title = string.Empty;
            // Act
            var result = validator.Validate(request);// Validate the request using the validator
            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ToList().First().ErrorMessage.ShouldBe(ResourceErrorMessages.TITLE_IS_REQUIRED);
            result.Errors.ShouldContain(e => e.PropertyName == "Title");


        }
        [Fact]
        public void Fail_When_Amount_Is_Zero()
        {
            // Arrange
            var validator = new ExpenseValidation();// Instantiate the validator
            var request = RequestExpenseJsonBuilder.Build();
            request.Amount = 0;
            // Act
            var result = validator.Validate(request);// Validate the request using the validator
            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ToList().First().ErrorMessage.ShouldBe(ResourceErrorMessages.THE_AMOUNT_MUST_THAN_ZERO);
            result.Errors.ShouldContain(e => e.PropertyName == "Amount");
        }
        [Fact]
        public void Fail_When_Date_Is_In_The_Future()
        {
            // Arrange
            var validator = new ExpenseValidation();// Instantiate the validator
            var request = RequestExpenseJsonBuilder.Build();
            request.Date = DateTime.Now.AddDays(1); // Set date to future
            // Act
            var result = validator.Validate(request);// Validate the request using the validator
            // Assert
            result.IsValid.ShouldBeFalse();
            result.Errors.ToList().First().ErrorMessage.ShouldBe(ResourceErrorMessages.EXPENSES_CANNOT_BE_FOR_THE_FUTURE);
            result.Errors.ShouldContain(e => e.PropertyName == "Date");
        }
        [Fact]
        public void Fail_When_PaymentType_Is_Invalid()
        {
            // Arrange
            var validator = new ExpenseValidation();// Instantiate the validator
            var request = RequestExpenseJsonBuilder.Build();
            request.PaymentType = (CashFlow.Communication.Enums.PaymentTypeEnum)999; // Invalid enum value
            // Act
            var result = validator.Validate(request);// Validate the request using the validator
            // Assert
            
            result.IsValid.ShouldBeFalse();
            result.Errors.ToList().First().ErrorMessage.ShouldBe(ResourceErrorMessages.PAYMENT_TYPE_IS_INVALID);
            result.Errors.ShouldContain(e => e.PropertyName == "PaymentType");
        }
        [Fact]
        public void Fail_Tag_Invalid()
        {
            var validator = new ExpenseValidation();
            var request = RequestExpenseJsonBuilder.Build();
            request.Tags.Add((CashFlow.Communication.Enums.TagEnum)122);


            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();

            result.Errors.ToList().First().ErrorMessage.ShouldBe(ResourceErrorMessages.TAG_TYPE_IS_NOT_SUPPORTED);


        }


    }

}
