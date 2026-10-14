using System.Numerics;

namespace NumberToWords;

public static class NumberToWordsConverter
{
    public static string Convert(decimal amount)
    {
        var isNegative = amount < 0;
        var absoluteAmount = Math.Abs(amount);
        var wholeAmount = decimal.Truncate(absoluteAmount);
        var dollars = new BigInteger(wholeAmount);
        var cents = (int)((absoluteAmount - wholeAmount) * 100);
        var dollarResult = NumberWordsDictionary.GetWords(dollars);
        var dollarWords = dollarResult.Words;
        var dollarUnit = dollarResult.IsPlural ? "dollars" : "dollar";
        var prefix = isNegative ? "negative " : string.Empty;

        if (cents == 0)
        {
            return $"{prefix}{dollarWords} {dollarUnit}";
        }

        var centWords = NumberWordsDictionary.GetWordsUnderHundred(cents)!.Words;
        var centUnit = cents == 1 ? "cent" : "cents";
        return $"{prefix}{dollarWords} {dollarUnit} and {centWords} {centUnit}";
    }
}
