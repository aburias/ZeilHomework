using zh.domain.Entities;

namespace zh.tests;

public class CreditCardTests
{
    [Fact]
    public void Should_Create_Valid_CreditCard()
    {
        var creditCard = new CreditCard("4539148803436467"); // Valid Luhn
        Assert.NotNull(creditCard);
        Assert.Equal("4539148803436467", creditCard.Number);
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Luhn_CreditCard()
    {
        Assert.Throws<ArgumentException>(() => new CreditCard("1234567812345678")); // Invalid Luhn
    }

    [Fact]
    public void Should_Throw_Exception_For_Non_Numeric_CreditCard()
    {
        Assert.Throws<ArgumentException>(() => new CreditCard("1234abcd5678"));
    }

    [Fact]
    public void Should_Throw_Exception_For_Empty_CreditCard()
    {
        Assert.Throws<ArgumentException>(() => new CreditCard(""));
    }
}