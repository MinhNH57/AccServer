var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;
var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
const string directory = "Configs";

builder.Configuration
    .AddJsonFile($"{directory}/reverseproxy.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"{directory}/reverseproxy.{environmentVariable}.json", optional: true, reloadOnChange: true);
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));



var app = builder.Build();

//app.MapDefaultEndpoints();

//app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.MapGet("/", () => "Smart gateway api moblie");
//app.UseMultiTenancy();
app.MapReverseProxy();

app.Run();
