var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;
var environmentVariable = builder.Environment.EnvironmentName;
const string directory = "Configs";

builder.Configuration
    .AddJsonFile($"{directory}/reverseproxy.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"{directory}/reverseproxy.{environmentVariable}.json", optional: true, reloadOnChange: true);
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddCors(opt =>
{
    var origins = configuration.GetValue<string>("AllowedOrigins") ?? string.Empty;

    opt.AddPolicy("web", policy =>
    {
        policy.WithOrigins(origins.Split(";", StringSplitOptions.RemoveEmptyEntries))
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();


app.UseRouting();
app.MapGet("/", () => "Smart gateway api");

app.UseCors();
app.MapReverseProxy();
app.Run();


