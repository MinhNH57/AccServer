using BuildingBlocks;
using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using BuildingBlocks.Web;
using Carter;
using Mapster;
using Microsoft.AspNetCore.ResponseCompression;
using Salary;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var configuration = builder.Configuration;

builder.AddCustomSerilog(env);
builder.Services.AddMultiTenancy(configuration);
TypeAdapterConfig.GlobalSettings.Default.ShallowCopyForSameType(false);
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});


builder.Services.AddCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddAspnetOpenApi();
builder.Services.AddJwt();
builder.AddDefaultHealthChecks();
builder.Services.AddCustomVersioning();

// Add module
builder.Services.AddSalaryModule(configuration);
builder.Services.AddCarter();


var app = builder.Build();
app.UseResponseCompression();

app.MapDefaultEndpoints();


app.MapDefaultMiddlware();
app.MapCarter();

app.MapGet("/test", async context => { await context.Response.WriteAsync("Salary Server API"); });
app.Run();
