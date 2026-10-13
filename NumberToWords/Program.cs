using NumberToWords;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/convert", NumberToWordsConverter.Convert);

app.Run();
