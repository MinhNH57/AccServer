using BuildingBlocks;
using BuildingBlocks.Caching;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Jwt;
using BuildingBlocks.Logging;
using BuildingBlocks.MultiTenancy;
using BuildingBlocks.OpenApi;
using Mapster;
using Microsoft.AspNetCore.ResponseCompression;
using Voucher;
using Voucher.Acc;
using Voucher.Acc.Apis;
using Voucher.HRM;
using Voucher.Sgas;

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

builder.Services.AddCors();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

//builder.ConfigureOpenTelemetry();

builder.Services.AddCaching(configuration);

builder.Services.AddVoucherModule(configuration);


var typeAcc = typeof(VoucherRoot).Assembly.GetTypes()
    .Where(t => typeof(ICarterModule).IsAssignableFrom(t)    
                && t.IsClass
                && !t.IsAbstract);
var typeSgas = typeof(VoucherSgasRoot).Assembly.GetTypes()
    .Where(t => typeof(ICarterModule).IsAssignableFrom(t)    
                && t.IsClass
                && !t.IsAbstract);
var typeHrm = typeof(VoucherHrmRoot).Assembly.GetTypes()
    .Where(t => typeof(ICarterModule).IsAssignableFrom(t)
                && t.IsClass
                && !t.IsAbstract);
builder.Services.AddCarter(configurator: cfg => cfg.WithModules(typeAcc.Concat(typeSgas.Concat(typeHrm)).ToArray()));

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
    opt.Providers.Add<GzipCompressionProvider>();
});
builder.Services.AddVoucherAcc(configuration);
builder.Services.AddVoucherSgas(configuration);

//builder.Services.AddCors();

var app = builder.Build();
app.MapCarter();
app.UseResponseCompression();

app.MapDefaultEndpoints();

app.MapVoucherApiV1();

app.MapDefaultMiddlware();


app.MapGet("/test", async context => { await context.Response.WriteAsync("Voucher Service API"); });

app.Run();
