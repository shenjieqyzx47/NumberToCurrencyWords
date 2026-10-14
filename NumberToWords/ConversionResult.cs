namespace NumberToWords;

public sealed record ConversionResult(bool IsSuccess, string? Value, string? Error)
{
    public static ConversionResult Ok(string value) => new(true, value, null);
    public static ConversionResult Fail(string error) => new(false, null, error);
}
