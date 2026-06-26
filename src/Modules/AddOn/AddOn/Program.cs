using AddOn;
using BuildingBlocks;
using Carter;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("smart-gas-station-e4599-firebase-adminsdk-fbsvc-d06ac88799.json")
});

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

builder.AddDefaultService(configuration, env);
builder.Services.AddAddOnModule(configuration);

builder.Services.AddCarter();
//builder.Services.AddResponseCompression(opt =>
//{
//    opt.EnableForHttps = true;
//    opt.Providers.Add<GzipCompressionProvider>();
//});
var app = builder.Build();
//app.UseResponseCompression();
app.MapDefaultEndpoints();
app.MapDefaultMiddlware();


app.MapCarter();

//app.MapGet("/", () => "Hello World!");


app.Run();
