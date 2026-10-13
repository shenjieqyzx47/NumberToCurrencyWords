using System.Numerics;

namespace NumberToWords;

public sealed record NumberWords(BigInteger Number, string Word);

public static class NumberWordsDictionary
{
    private static readonly NumberWords One = new NumberWords(1, "one");
    private static readonly NumberWords Two = new NumberWords(2, "two");
    private static readonly NumberWords Three = new NumberWords(3, "three");
    private static readonly NumberWords Four = new NumberWords(4, "four");
    private static readonly NumberWords Five = new NumberWords(5, "five");
    private static readonly NumberWords Six = new NumberWords(6, "six");
    private static readonly NumberWords Seven = new NumberWords(7, "seven");
    private static readonly NumberWords Eight = new NumberWords(8, "eight");
    private static readonly NumberWords Nine = new NumberWords(9, "nine");
    private static readonly NumberWords Ten = new NumberWords(10, "ten");
    private static readonly NumberWords Eleven = new NumberWords(11, "eleven");
    private static readonly NumberWords Twelve = new NumberWords(12, "twelve");
    private static readonly NumberWords Thirteen = new NumberWords(13, "thirteen");
    private static readonly NumberWords Fourteen = new NumberWords(14, "fourteen");
    private static readonly NumberWords Fifteen = new NumberWords(15, "fifteen");
    private static readonly NumberWords Sixteen = new NumberWords(16, "sixteen");
    private static readonly NumberWords Seventeen = new NumberWords(17, "seventeen");
    private static readonly NumberWords Eighteen = new NumberWords(18, "eighteen");
    private static readonly NumberWords Nineteen = new NumberWords(19, "nineteen");
    private static readonly NumberWords Twenty = new NumberWords(20, "twenty");
    private static readonly NumberWords Thirty = new NumberWords(30, "thirty");
    private static readonly NumberWords Forty = new NumberWords(40, "forty");
    private static readonly NumberWords Fifty = new NumberWords(50, "fifty");
    private static readonly NumberWords Sixty = new NumberWords(60, "sixty");
    private static readonly NumberWords Seventy = new NumberWords(70, "seventy");
    private static readonly NumberWords Eighty = new NumberWords(80, "eighty");
    private static readonly NumberWords Ninety = new NumberWords(90, "ninety");
    private static readonly NumberWords Hundred = new NumberWords(100, "hundred");
    private static readonly NumberWords Thousand = new NumberWords(1000, "thousand");
    private static readonly NumberWords Million = new NumberWords(1000000, "million");
    private static readonly NumberWords Billion = new NumberWords(1000000000, "billion");
    private static readonly NumberWords Trillion = new NumberWords(1000000000000, "trillion");
    private static readonly NumberWords Quadrillion = new NumberWords(1000000000000000, "quadrillion");
    private static readonly NumberWords Quintillion = new NumberWords(1000000000000000000, "quintillion");

    private static List<NumberWords> NumberWordsList { get; } =
    [
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        Seventeen,
        Eighteen,
        Nineteen,
        Twenty,
        Thirty,
        Forty,
        Fifty,
        Sixty,
        Seventy,
        Eighty,
        Ninety,
        Hundred,
        Thousand,
        Million,
        Billion,
        Trillion,
        Quadrillion,
        Quintillion
    ];

    private static readonly List<string> Scales =
    [
        "thousand",
        "million",
        "billion",
        "trillion",
        "quadrillion",
        "quintillion"
    ];

    public static GetWordsResult? GetWordsUnderThousands(BigInteger number)
    {
        if (number < 100) return GetWordsUnderHundred(number);
        if (number >= 1000) return null;
        var hundred = number / 100;
        var hundredInWord = NumberWordsList.SingleOrDefault(x => x.Number == hundred)?.Word ?? "Not supported";
        var remainder = number % 100;
        var remainderInWords = GetWordsUnderHundred(remainder);
        return remainderInWords is null
            ? new GetWordsResult(true, $"{hundredInWord} hundred")
            : new GetWordsResult(true, $"{hundredInWord} hundred and {remainderInWords.Words}");
    }

    public static GetWordsResult? GetWordsUnderHundred(BigInteger number)
    {
        if (number == 0)
        {
            return null;
        }

        if (number == 1)
        {
            return new GetWordsResult(false, NumberWordsList.Single(x => x.Number == number).Word);
        }

        if (number < 20)
        {
            return new GetWordsResult(true, NumberWordsList.Single(x => x.Number == number).Word);
        }

        if (number >= 100) return null;
        {
            var tens = (number / 10) * 10;
            var remainder = number % 10;
            var tensInWord = NumberWordsList.Single(x => x.Number == tens).Word;
            if (remainder == 0)
            {
                return new GetWordsResult(true, tensInWord);
            }

            var remainderInWord = NumberWordsList.Single(x => x.Number == remainder).Word;

            return new GetWordsResult(true, $"{tensInWord}-{remainderInWord}");
        }
    }

    public static GetWordsResult GetWords(BigInteger number)
    {
        if (number == 0)
            return new GetWordsResult(false, "Zero");

        if (number == 1)
            return new GetWordsResult(false, NumberWordsList.Single(x => x.Number == number).Word);
        var result = new List<string>();

        var numberInRanges = new List<BigInteger>();
        while (number > 0)
        {
            numberInRanges.Add(number % 1000);
            number /= 1000;
        }

        for (var index = 0; index < numberInRanges.Count; index++)
        {
            var numberInRangeWords = GetWordsUnderThousands(numberInRanges[index]);
            if (numberInRangeWords is null)
            {
                continue;
            }

            var scale = index == 0 ? null : Scales[index - 1];

            var numberInRangeWordsWithScale =
                scale is null ? numberInRangeWords.Words : $"{numberInRangeWords.Words} {scale}";
            result.Add(numberInRangeWordsWithScale);
        }

        var resultInOrder = result.AsEnumerable().Reverse().ToList();
        var lastRange = resultInOrder.Last();
        var otherRanges = resultInOrder.Take(resultInOrder.Count - 1).ToList();
        if (result.Count > 1 && !lastRange.Contains("and "))
        {
            return new GetWordsResult(true, $"{string.Join(", ", otherRanges)} and {lastRange}");
        }

        return new GetWordsResult(true, string.Join(", ", resultInOrder));
    }

    public sealed record GetWordsResult(bool IsPlural, string Words);
}