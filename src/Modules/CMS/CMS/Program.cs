using BuildingBlocks;
using CMS;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

builder.AddDefaultService(configuration, env);

builder.Services.AddCmsModule();
builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();

app.UseResponseCompression();
app.MapDefaultEndpoints();

app.MapDefaultMiddlware();
//app.UseSerilogRequestLogging();
////app.UseRouting();
//app.UseExceptionHandler();
//app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


//var cms = app.NewVersionedApi("Cms");
//cms.MapCmsApiV1()
//    .RequireAuthorization("ApiScope");
//app.MapCmsApiV1().RequireAuthorization();
//app.MapGet("/", () => "Hello World!");


//app.UseMultiTenancy();

app.Run();
