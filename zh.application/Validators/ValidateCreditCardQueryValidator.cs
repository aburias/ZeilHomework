using FluentValidation;
using zh.application.Queries.ValidateCreditCard;
using zh.domain.Entities;

namespace zh.application.Validators;

public class ValidateCreditCardQueryValidator : AbstractValidator<ValidateCreditCardQuery>
{
    public ValidateCreditCardQueryValidator()
    {
        RuleFor(x => x.CreditCardNumber)
            .NotEmpty().WithMessage("Credit card number cannot be empty.")
            .Must(x => x.All(char.IsDigit)).WithMessage("Credit card number must be numeric.")
            .Must(BeValidLuhn).WithMessage("Credit card number is not valid according to the Luhn algorithm.");
    }

    private bool BeValidLuhn(string creditCardNumber)
    {
        try
        {
            var creditCard = new CreditCard(creditCardNumber);
            return true;
        }
        catch
        {
            return false;
        }
    }
}