var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/who", () => "Зарембицький Єгор");
app.MapGet("/time", () => GetTime());

string GetTime()
{
    DateTimeOffset time = DateTimeOffset.Now;
    return time.ToString();
}

app.Run();
