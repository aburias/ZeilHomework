using zh.application.Queries.ValidateCreditCard;

namespace zh.tests;

public class ValidateCreditCardQueryHandlerTests
{
    private readonly ValidateCreditCardQueryHandler _handler;

    public ValidateCreditCardQueryHandlerTests()
    {
        _handler = new ValidateCreditCardQueryHandler();
    }

    [Fact]
    public async Task Should_Return_True_For_Valid_CreditCard()
    {
        var query = new ValidateCreditCardQuery("4539148803436467"); // Valid Luhn
        var result = await _handler.Handle(query, default);

        Assert.True(result);
    }

    [Fact]
    public async Task Should_Return_False_For_Invalid_CreditCard()
    {
        var query = new ValidateCreditCardQuery("1234567812345678"); // Invalid Luhn
        var result = await _handler.Handle(query, default);

        Assert.False(result);
    }
}