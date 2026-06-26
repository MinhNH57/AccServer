using System.Reflection;
using EmailGGModule.Service;

namespace EmailGGModule
{
    public static class EmailModule
    {
        public static IServiceCollection AddEmailModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
