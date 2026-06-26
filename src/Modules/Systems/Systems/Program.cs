using BuildingBlocks;
using BuildingBlocks.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Systems;
using Systems.Apis;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;


builder.AddDefaultService(configuration, env);
builder.Services.AddSystemModule(configuration);
builder.Services.AddCarter();

//builder.Services.AddScoped<ICurrentUser, CurrentUser>(); 
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



app.MapDefaultMiddlware();

app.MapCmsApiV1().RequireAuthorization();

app.MapCarter();
app.UseCors("AllowAll");

app.Run();
