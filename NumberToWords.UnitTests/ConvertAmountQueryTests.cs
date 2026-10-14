using Xunit;

namespace NumberToWords.UnitTests;

public sealed class ConvertAmountQueryTests
{
    private readonly ConvertAmountQuery _query = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenInputIsNullOrEmpty_Execute_ShouldReturnFailure(string? input)
    {
        var result = _query.Handle(input);

        Assert.False(result.IsSuccess);
        Assert.Equal("No input received.", result.Error);
    }

    [Theory]
    [InlineData("123.46567")]
    [InlineData("123.4567")]
    [InlineData("123.4567.89")]
    [InlineData("123.45.67.89")]
    [InlineData("not a number")]
    [InlineData("124162784834568956905600506706124162784834568956905600506706")]
    public void WhenInputIsInvalid_Execute_ShouldReturnFailure(string input)
    {
        var result = _query.Handle(input);

        Assert.False(result.IsSuccess);
        Assert.Equal("Enter a monetary amount (up to two decimal places).", result.Error);
    }

    [Theory]
    [InlineData("123.31", "one hundred and twenty-three dollars and thirty-one cents")]
    [InlineData("68092", "sixty-eight thousand and ninety-two dollars")]
    [InlineData("-235.07", "negative two hundred and thirty-five dollars and seven cents")]
    [InlineData("1.1", "one dollar and ten cents")]
    [InlineData("-123.88", "negative one hundred and twenty-three dollars and eighty-eight cents")]
    public void WhenInputIsValid_Execute_ShouldReturnWords(string input, string expected)
    {
        var result = _query.Handle(input);

        Assert.True(result.IsSuccess);
        Assert.Equal(expected, result.Value);
        Assert.Null(result.Error);
    }
}
