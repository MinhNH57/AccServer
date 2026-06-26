using BuildingBlocks;
using BuildingBlocks.Behaviors;
using Carter;
using Catalog.Base;
using Catalog.Base.Features;
using Catalog.Base.GenarateCode;
using Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogAccountSymbol;
using Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogRoom;
using Catalog.Features.Web.Catalog.Commands.CatalogLogic.CatalogSelecttedPrint;
using Catalog.Features.Web.Fund.Excel.Api;
using Catalog.HRM;
using Catalog.HRM.Features;
using Catalog.SGas;
using Catalog.SGas.Features;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;
var assembly = typeof(Program).Assembly;

//builder.Configuration.AddConfiguration(ConfigurationHelper.GetConfiguration());
builder.AddDefaultService(configuration, env);

//builder.Services.AddCatalogModule(configuration);

builder.Services.AddCarter(configurator: cfg => cfg.WithModules(typeof(CatalogApi), typeof(CatalogUnitApi), typeof(HrmCatalogApi), typeof(HrmSyncApi), typeof(AccountSymbolApi), typeof(CatalogSelecttedPrintApi),typeof(CatalogRoomApi),typeof(ExcelApi), typeof(DapperApi),typeof(GenerateCodeApi), typeof(Api)));

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    //config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
});

builder.Services
    .AddCatalogHrm(configuration)
    .AddCatalogSGas(configuration);

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
