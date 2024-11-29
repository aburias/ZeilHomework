namespace zh.domain.Entities;

public class CreditCard
{
    public string Number { get; private set; }

    public CreditCard(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || !number.All(char.IsDigit))
            throw new ArgumentException("Credit card number must be numeric and not empty.");

        if (!IsValidLuhn(number))
            throw new ArgumentException("Credit card number is not valid according to the Luhn algorithm.");

        Number = number;
    }

    private static bool IsValidLuhn(string number)
    {
        var sum = 0;
        var alternate = false;

        for (var i = number.Length - 1; i >= 0; i--)
        {
            var digit = number[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }
}