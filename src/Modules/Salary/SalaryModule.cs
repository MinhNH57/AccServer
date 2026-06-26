using BuildingBlocks.Behaviors;
using BuildingBlocks.Dapper;
using BuildingBlocks.Mapster;
using BuildingBlocks.Web;
using FluentValidation;
using Salary.Infrastructure;

namespace Salary
{
    public static class SalaryModule
    {
        public static IServiceCollection AddSalaryModule(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(Program).Assembly;
            services.AddDbContext<SalaryDbContext>();
            services.AddScoped<SmartDataServices>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddCustomMapster(typeof(SalaryRoot).Assembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(assembly);


            services.AddHttpContextAccessor();

            return services;
        }
    }
}
