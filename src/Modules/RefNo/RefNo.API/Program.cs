using BuildingBlocks;
using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using RefNo.API;
using RefNo.API.Apis;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;
//builder.AddDefaultService(configuration, env);
TypeAdapterConfig.GlobalSettings.Default.ShallowCopyForSameType(false);

builder.AddCustomSerilog(env);
builder.Services.AddMultiTenancy(configuration);
//builder.Services.AddCustomMapster(assembly);
builder.AddDefaultHealthChecks();

builder.Services.AddJwt();

builder.Services.AddCustomVersioning();

builder.Services.AddAspnetOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddCaching(configuration);

builder.Services.AddRefNoModule(configuration);

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

app.MapRefNoApiV1();

app.MapDefaultMiddlware();

app.UseCors("AllowAll");

app.MapGet("/test", async context => { await context.Response.WriteAsync("RefNo Server API"); });

app.Run();
