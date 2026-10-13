namespace NumberToWords;

public static class NumberToWordsConverter
{
    public static string Convert(string? input)
    {
        // filter null or empty input
        if (string.IsNullOrEmpty(input))
        {
            return "No input received.";
        }

        var canParse = decimal.TryParse(input, out var number);
        var parts = input.Split('.');
        if (!canParse || (parts.Length > 1 && parts[1].Length > 2))
        {
            return "Enter a monetary amount (up to two decimal places).";
        }

        var isNegative = number < 0;

        var dollarNumber = (int)decimal.Round(isNegative ? number * -1 : number, MidpointRounding.AwayFromZero);
        var centNumber = (int)(((isNegative ? number * -1 : number) - dollarNumber) * 100);

        var dollarWords = NumberWordsDictionary.GetWords(dollarNumber);
        var centWords = NumberWordsDictionary.GetWordsUnderHundred(centNumber);
        var dollarUnit = dollarWords.IsPlural ? "dollars" : "dollar";

        if (centWords is null)
        {
            return isNegative ? $"negative {dollarWords.Words} {dollarUnit}" : $"{dollarWords.Words} {dollarUnit}";
        }

        var centUnit = centWords.IsPlural ? "cents" : "cent";
        return isNegative
            ? $"negative {dollarWords.Words} {dollarUnit} and {centWords.Words} {centUnit}"
            : $"{dollarWords.Words} {dollarUnit} and {centWords.Words} {centUnit}";
    }
}