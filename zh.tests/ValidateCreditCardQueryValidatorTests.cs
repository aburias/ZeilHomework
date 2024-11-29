using FluentValidation.TestHelper;
using zh.application.Queries.ValidateCreditCard;
using zh.application.Validators;

namespace zh.tests;

public class ValidateCreditCardQueryValidatorTests
{
    private readonly ValidateCreditCardQueryValidator _validator;

    public ValidateCreditCardQueryValidatorTests()
    {
        _validator = new ValidateCreditCardQueryValidator();
    }

    [Fact]
    public void Should_Have_Error_When_CreditCardNumber_Is_Empty()
    {
        var query = new ValidateCreditCardQuery(string.Empty);
        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CreditCardNumber)
            .WithErrorMessage("Credit card number cannot be empty.");
    }

    [Fact]
    public void Should_Have_Error_When_CreditCardNumber_Is_Not_Numeric()
    {
        var query = new ValidateCreditCardQuery("1234abcd5678");
        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CreditCardNumber)
            .WithErrorMessage("Credit card number must be numeric.");
    }

    [Fact]
    public void Should_Have_Error_When_CreditCardNumber_Is_Invalid_Luhn()
    {
        var query = new ValidateCreditCardQuery("1234567812345678"); // Invalid Luhn
        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CreditCardNumber)
            .WithErrorMessage("Credit card number is not valid according to the Luhn algorithm.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_CreditCardNumber_Is_Valid()
    {
        var query = new ValidateCreditCardQuery("4539148803436467"); // Valid Luhn
        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.CreditCardNumber);
    }
}