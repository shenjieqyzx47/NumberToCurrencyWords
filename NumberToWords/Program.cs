using NumberToWords;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<ConvertAmountCommand>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/convert", (string? input, ConvertAmountCommand command) =>
{
    var result = command.Handle(input);
    if (result.IsSuccess)
    {
        return Results.Text(result.Value!);
    }

    return Results.Problem(
        statusCode: StatusCodes.Status400BadRequest,
        title: "Invalid amount",
        detail: result.Error);
});

app.Run();
