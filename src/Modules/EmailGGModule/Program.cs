using EmailGGModule;
using EmailGGModule.Models;
var builder = WebApplication.CreateBuilder(args);
const string directory = "Configs";
var configuration = builder.Configuration;
// Thêm dịch vụ CORS
builder.Services.AddCors(opt =>
{
    var origins = configuration.GetValue<string>("AllowedOrigins") ?? string.Empty;

    opt.AddPolicy("web", policy =>
    {
        policy.WithOrigins(origins.Split(";", StringSplitOptions.RemoveEmptyEntries))
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddEmailModule(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("web");

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();

app.Run();
