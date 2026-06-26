using BuildingBlocks;
using BuildingBlocks.Middleware;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
var configuration = builder.Configuration;
var env = builder.Environment;

builder.AddDefaultService(configuration, env);

builder.Services.AddCarter();


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("LogDb")!);
}).UseLightweightSessions();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});


var app = builder.Build();

app.MapDefaultEndpoints();
app.UseExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<CurrentUserMiddlware>();
//app.MapGet("/", () => "Hello World!");
app.MapCarter();
app.Run();
