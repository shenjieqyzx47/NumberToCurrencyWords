using Xunit;

namespace NumberToWords.UnitTests;

public sealed class NumberToWordsConverterTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenInputIsNullOrEmpty_ConvertToWords_ShouldReturnError(string? input)
    {
        // Arrange
        var expected = "No input received.";

        // Act
        var result = NumberToWordsConverter.Convert(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123.46567")]
    [InlineData("123.4567")]
    [InlineData("123.4567.89")]
    [InlineData("123.45.67.89")]
    [InlineData("not a number")]
    [InlineData("124162784834568956905600506706124162784834568956905600506706")]
    public void WhenInputIsInvalid_ConvertToWords_ShouldReturnError(string input)
    {
        // Arrange
        var expected = "Enter a monetary amount (up to two decimal places).";

        // Act
        var result = NumberToWordsConverter.Convert(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("123.31", "one hundred and twenty-three dollars and thirty-one cents")]
    [InlineData("68092", "sixty-eight thousand and ninety-two dollars")]
    [InlineData("-235.07", "negative two hundred and thirty-five dollars and seven cents")]
    [InlineData("1.1", "one dollar and ten cents")]
    public void WhenInputIsValid_ConvertToWords_ShouldReturnCorrectResult(string input, string expected)
    {
        // Act
        var result = NumberToWordsConverter.Convert(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
}