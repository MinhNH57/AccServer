using BuildingBlocks;
using Carter;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Notification;

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("smart-gas-station-e4599-firebase-adminsdk-fbsvc-d06ac88799.json")
});

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;


//builder.Configuration.AddConfiguration(ConfigurationHelper.GetConfiguration());
builder.AddDefaultService(configuration, env);

builder.Services.AddCarter();
builder.Services.AddNotificationModule(configuration);

var app = builder.Build();
app.MapDefaultEndpoints();


app.MapDefaultMiddlware();

app.MapCarter();

//app.MapGet("/", () => "Hello World!");

app.Run();
