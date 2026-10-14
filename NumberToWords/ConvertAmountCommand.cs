using System.Globalization;

namespace NumberToWords;

public sealed class ConvertAmountCommand
{
    private const string EmptyInputMessage = "No input received.";
    private const string InvalidAmountMessage = "Enter a monetary amount (up to two decimal places).";
    private const decimal MaximumSupportedAmount = 1_000_000_000_000_000_000_000m;

    public ConversionResult Handle(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return ConversionResult.Fail(EmptyInputMessage);
        }

        if (!TryParseAmount(input, out var amount))
        {
            return ConversionResult.Fail(InvalidAmountMessage);
        }

        return ConversionResult.Ok(NumberToWordsConverter.Convert(amount));
    }

    private static bool TryParseAmount(string input, out decimal amount)
    {
        amount = 0;
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

        return amount > -MaximumSupportedAmount && amount < MaximumSupportedAmount;
    }
}
