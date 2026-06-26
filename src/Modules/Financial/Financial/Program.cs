using BuildingBlocks;
using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using BuildingBlocks.Web;
using Financial;
using Financial.Apis;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;

//builder.AddDefaultService(configuration, env);

builder.AddCustomSerilog(env);
builder.Services.AddMultiTenancy(configuration);
//builder.Services.AddCustomMapster(assembly);
builder.AddDefaultHealthChecks();

builder.Services.AddJwt();

builder.Services.AddCustomVersioning();

builder.Services.AddAspnetOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

//builder.ConfigureOpenTelemetry();

builder.Services.AddCaching(configuration);

builder.Services.AddFinancialModule(configuration);

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();
app.UseResponseCompression();

app.MapDefaultEndpoints();

app.MapFinancialApiV1();

app.MapDefaultMiddlware();

app.UseCors();

app.MapGet("/test", async context => { await context.Response.WriteAsync("Financial Server API"); });

app.Run();
