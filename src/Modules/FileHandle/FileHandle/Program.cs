using BoldReports.Web;
using BuildingBlocks;
using BuildingBlocks.Middleware;
using BuildingBlocks.MultiTenancy;
using FileHandle;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
builder.AddDefaultService(configuration, env);

builder.Services.AddFileHandleModule(configuration);

var app = builder.Build();
ReportConfig.DefaultSettings = new ReportSettings().RegisterExtensions(new List<string> { "BoldReports.CRI.Barcode" });

app.MapDefaultEndpoints();
//app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = new PathString("/Uploads")
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Reports")),
    RequestPath = new PathString("/Reports")
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Downloads")),
    RequestPath = new PathString("/Downloads")
});

app.UseMultiTenancy();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddlware>();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.

app.MapControllers();
app.Run();
