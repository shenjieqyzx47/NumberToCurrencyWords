using System.Numerics;
using Xunit;

namespace NumberToWords.UnitTests;

public sealed class NumberWordsDictionaryTests
{
    [Theory]
    [InlineData(1, "one", false)]
    [InlineData(2, "two", true)]
    [InlineData(3, "three", true)]
    [InlineData(4, "four", true)]
    [InlineData(5, "five", true)]
    [InlineData(6, "six", true)]
    [InlineData(7, "seven", true)]
    [InlineData(8, "eight", true)]
    [InlineData(9, "nine", true)]
    [InlineData(10, "ten", true)]
    [InlineData(11, "eleven", true)]
    [InlineData(12, "twelve", true)]
    [InlineData(13, "thirteen", true)]
    [InlineData(14, "fourteen", true)]
    [InlineData(15, "fifteen", true)]
    [InlineData(16, "sixteen", true)]
    [InlineData(17, "seventeen", true)]
    [InlineData(18, "eighteen", true)]
    [InlineData(19, "nineteen", true)]
    [InlineData(20, "twenty", true)]
    [InlineData(30, "thirty", true)]
    [InlineData(41, "forty-one", true)]
    [InlineData(66, "sixty-six", true)]
    [InlineData(99, "ninety-nine", true)]
    public void GetWordsUnderHundredTests(int number, string expectedWords, bool isPlural)
    {
        // Act
        var result = NumberWordsDictionary.GetWordsUnderHundred(number);

        // Arrange
        Assert.Equal(expectedWords, result.Words);
        Assert.Equal(isPlural, result.IsPlural);
    }

    [Theory]
    [InlineData(101, "one hundred and one")]
    [InlineData(555, "five hundred and fifty-five")]
    [InlineData(600, "six hundred")]
    [InlineData(928, "nine hundred and twenty-eight")]
    public void GetWordsUnderThousandTests(int number, string expected)
    {
        // Act
        var result = NumberWordsDictionary.GetWordsUnderThousands(number);

        // Arrange
        Assert.Equal(expected, result.Words);
    }


    [Theory]
    [InlineData(1000000, "one million")]
    [InlineData(10050000, "ten million and fifty thousand")]
    [InlineData(1000001, "one million and one")]
    [InlineData(163947234,
        "one hundred and sixty-three million, nine hundred and forty-seven thousand, two hundred and thirty-four")]
    [InlineData(738406219,
        "seven hundred and thirty-eight million, four hundred and six thousand, two hundred and nineteen")]
    [InlineData(582913704,
        "five hundred and eighty-two million, nine hundred and thirteen thousand, seven hundred and four")]
    [InlineData(642780010123060,
        "six hundred and forty-two trillion, seven hundred and eighty billion, ten million, one hundred and twenty-three thousand and sixty")]
    public void GetWordsUnderBillionsTests(BigInteger number, string expected)
    {
        // Act
        var result = NumberWordsDictionary.GetWords(number);

        // Arrange
        Assert.Equal(expected, result.Words);
    }
}