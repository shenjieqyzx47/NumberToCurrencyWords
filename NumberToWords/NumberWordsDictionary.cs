using System.Numerics;

namespace NumberToWords;

/// <summary>Converts non-negative integers into English words.</summary>
public static class NumberWordsDictionary
{
    private static readonly string[] OnesAndTeens =
    [
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen",
        "eighteen", "nineteen"
    ];

    private static readonly string[] Tens =
    [
        "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
    ];

    private static readonly string[] Scales =
    [
        "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion"
    ];

    public static GetWordsResult? GetWordsUnderHundred(BigInteger number)
    {
        if (number <= 0 || number >= 100)
        {
            return null;
        }

        var value = (int)number;
        var words = value < 20
            ? OnesAndTeens[value]
            : value % 10 == 0
                ? Tens[value / 10]
                : $"{Tens[value / 10]}-{OnesAndTeens[value % 10]}";

        return new GetWordsResult(value != 1, words);
    }

    public static GetWordsResult? GetWordsUnderThousands(BigInteger number)
    {
        if (number < 0 || number >= 1000)
        {
            return null;
        }

        if (number < 100)
        {
            return GetWordsUnderHundred(number);
        }

        var hundreds = (int)(number / 100);
        var remainder = number % 100;
        var words = $"{OnesAndTeens[hundreds]} hundred";

        if (remainder > 0)
        {
            words += $" and {GetWordsUnderHundred(remainder)!.Words}";
        }

        return new GetWordsResult(true, words);
    }

    public static GetWordsResult GetWords(BigInteger number)
    {
        if (number == 0)
        {
            return new GetWordsResult(false, "Zero");
        }

        if (number == 1)
        {
            return new GetWordsResult(false, OnesAndTeens[1]);
        }

        if (number < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Number must be non-negative.");
        }

        var groups = new List<string>();
        var scaleIndex = 0;

        while (number > 0)
        {
            var group = number % 1000;
            if (group > 0)
            {
                if (scaleIndex >= Scales.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), "Number exceeds the supported quintillion scale.");
                }

                var groupWords = GetWordsUnderThousands(group)!.Words;
                var scale = Scales[scaleIndex];
                groups.Add(string.IsNullOrEmpty(scale) ? groupWords : $"{groupWords} {scale}");
            }

            number /= 1000;
            scaleIndex++;
        }

        groups.Reverse();
        var finalGroup = groups[^1];
        var precedingGroups = groups.Take(groups.Count - 1);

        if (groups.Count > 1 && !finalGroup.Contains("and ", StringComparison.Ordinal))
        {
            return new GetWordsResult(true, $"{string.Join(", ", precedingGroups)} and {finalGroup}");
        }

        return new GetWordsResult(true, string.Join(", ", groups));
    }

    public sealed record GetWordsResult(bool IsPlural, string Words);
}
