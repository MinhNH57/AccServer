
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

const string directory = "Configs";
builder.Configuration.AddJsonFile($"{directory}/reverseproxy.json").AddEnvironmentVariables();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddAuthentication(BearerTokenDefaults.AuthenticationScheme).AddJwtBearer(BearerTokenDefaults.AuthenticationScheme);
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("customPolicy", policy =>
//        policy.RequireAuthenticatedUser());
//});


//builder.Services.AddCarter();
var app = builder.Build();
//app.MapDefaultEndpoints();

//app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.MapGet("/", () => "Smart gateway api");
//app.UseMultiTenancy();
app.MapReverseProxy();
app.Run();
