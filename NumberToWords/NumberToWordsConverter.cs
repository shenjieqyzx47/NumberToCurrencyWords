using System.Globalization;
using System.Numerics;

namespace NumberToWords;

public static class NumberToWordsConverter
{
    private const string EmptyInputMessage = "No input received.";
    private const string InvalidAmountMessage = "Enter a monetary amount (up to two decimal places).";

    public static string Convert(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return EmptyInputMessage;
        }

        if (!TryParseAmount(input, out var amount))
        {
            return InvalidAmountMessage;
        }

        var isNegative = amount < 0;
        var absoluteAmount = Math.Abs(amount);
        var dollars = new BigInteger(decimal.Truncate(absoluteAmount));
        var cents = (int)((absoluteAmount - decimal.Truncate(absoluteAmount)) * 100);
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

    private static bool TryParseAmount(string input, out decimal amount)
    {
        amount = default;
        var decimalPoint = input.IndexOf('.');
        if (decimalPoint >= 0 && input.Length - decimalPoint - 1 > 2)
        {
            return false;
        }

        if (!decimal.TryParse(
            input,
            NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture,
            out amount))
        {
            return false;
        }

        return Math.Abs(amount) < 1_000_000_000_000_000_000_000m;
    }
}
