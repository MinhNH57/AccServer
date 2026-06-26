using BuildingBlocks;
using BuildingBlocks.Middleware;
using Carter;
using Identity;
using Identity.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;


#region Cmt

//builder.Services.AddIdentityServer(opts =>
//    {
//        //opts.KeyManagement.KeyPath = "/keys";
//        //// new key every 30 days
//        //opts.KeyManagement.RotationInterval = TimeSpan.FromDays(30);

//        //// announce new key 2 days in advance in discovery
//        //opts.KeyManagement.PropagationTime = TimeSpan.FromDays(2);

//        //// keep old key for 7 days in discovery for validation of tokens
//        //opts.KeyManagement.RetentionDuration = TimeSpan.FromDays(7);

//        //// don't delete keys after their retention period is over
//        //opts.KeyManagement.DeleteRetiredKeys = false;

//        opts.KeyManagement.Enabled = false;
//    })
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    //.AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddInMemoryClients(Config.Clients)
//    .AddDeveloperSigningCredential()
//    .AddAuthorizeInteractionResponseGenerator<TestResponseGenerator>();
#endregion


builder.AddDefaultService(configuration, env);
builder.Services.AddCarter();


builder.Services.AddIdentityModule(configuration);

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

app.MapDefaultEndpoints();

//app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddlware>();

//app.MapIdenityApiV1().RequireAuthorization();

app.MapCarter();
//app.MapGet("/", () => "Identity module");

//app.UseIdentityServer();
app.UseMultiTenancy();

app.Run();

