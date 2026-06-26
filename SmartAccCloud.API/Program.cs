using Finbuckle.MultiTenant;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using SmartAccCloud.Application;
using Serilog;
using SmartAccCloud.API;
using SmartAccCloud.API.Middlewares;
using SmartAccCloud.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("smart-gas-station-e4599-firebase-adminsdk-fbsvc-d06ac88799.json")
});

var builder = WebApplication.CreateBuilder(args);

//Write log
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomOperationIds(c => $"{c.ActionDescriptor.RouteValues["action"]}");
    c.UseAllOfToExtendReferenceSchemas();
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddApiService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseMultiTenant();
app.UserApiServices();
app.UseInfrastructure();
app.UseMiddleware<CurrentUserMiddlware>();
app.Use(async (context, next) =>
{
    context.Features.Get<IHttpMaxRequestBodySizeFeature>()!.MaxRequestBodySize = 10 * 1024 * 1024; // 20MB
    await next.Invoke();
});

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = new PathString("/Uploads")
});
app.MapControllers();

app.Run();
