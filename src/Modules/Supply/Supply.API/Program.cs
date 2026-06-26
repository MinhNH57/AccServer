using BuildingBlocks;
using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

builder.AddCustomSerilog(env);
builder.Services.AddMultiTenancy(configuration);

builder.AddDefaultHealthChecks();

builder.Services.AddJwt();

builder.Services.AddCustomVersioning();

builder.Services.AddAspnetOpenApi();

builder.Services.AddCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.AddApplicationServices();

builder.Services.AddProblemDetails();

builder.Services.AddCaching(configuration);

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseResponseCompression();

app.MapDefaultEndpoints();

app.MapSupplyApiV1();

app.MapDefaultMiddlware();

app.UseCors("AllowAll");

app.MapGet("/test", async context => { await context.Response.WriteAsync("Supply Server API"); });

app.Run();
