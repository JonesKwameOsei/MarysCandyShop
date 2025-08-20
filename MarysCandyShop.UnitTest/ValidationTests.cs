namespace MarysCandyShop.UnitTest;

public class ValidationTests
{
    [Fact]
    public void WhenStringIsValidReturnTrue()
    {
        var stringInput = "Test Chocolate Bar";
        var result = Validation.IsStringValid(stringInput);

        Assert.True(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("string with more than 20 characters")]
    public void WhenStringIsNotValidReturnFalse(string testString)
    {
        var result = Validation.IsStringValid(testString);
        Assert.False(result);
    }

    [Theory]
    [InlineData("20")]
    [InlineData("20.5")]
    public void WhenPriceIsValidReturnFalse(string testPrice)
    {
        var result = Validation.IsPriceValid(testPrice);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("-1")]
    [InlineData("11000")]
    [InlineData("393837383939393939383839393938383838")]
    public void WhenPriceIsNotValidReturnFalse(string testPrice)
    {
        var result = Validation.IsPriceValid(testPrice);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("-1")]
    [InlineData("100")]
    [InlineData("393837383939393939383839393938383838")]
    [InlineData("20.5")]
    public void WhenCocoaIsNotValidReturnFalse(string testCocoaInput)
    {
        var result = Validation.IsCocoaInputValid(testCocoaInput);
        Assert.False(result.IsValid);
    }
}
