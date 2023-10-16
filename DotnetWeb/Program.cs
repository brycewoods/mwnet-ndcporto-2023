var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (string audience) 
    => new Greeting("Bom dia", audience ?? "NDC Porto"));

app.Run();

public class Greeting
{
    public string Message { get; set; }
    public string Audience { get; set; }
    public DateTimeOffset DateTimeOffset { get;set;}

    public Greeting(string message, string audience)
    {
        this.Message = message;
        this.Audience = audience;
        this.DateTimeOffset = DateTimeOffset.UtcNow;
    }
}
