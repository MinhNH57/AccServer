using BuildingBlocks;
using Carter;
using Microsoft.AspNetCore.ResponseCompression;
using Report;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;

builder.AddDefaultService(configuration, env);

builder.Services.AddReportModule(configuration);
builder.Services.AddCarter();

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();
app.UseResponseCompression();
app.MapDefaultEndpoints();

app.MapDefaultMiddlware();
app.MapCarter();
app.Run();