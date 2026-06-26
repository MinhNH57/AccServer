using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMesageBroker(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        services.AddMassTransit(config =>
        {
            config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(environmentVariable, false));
 
            if (assembly != null)
            {
                config.AddConsumers(assembly);
            }

            var messs = configuration.GetSection("MessageBroker").Value;
   
            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri("amqp://rbmq.ssoftware.vn:5672"), host =>
                {
                    host.Username("smartdev");
                    host.Password("SP@ssw0rd%");
                });
                configurator.ConfigureEndpoints(context);
                configurator.UsePublishFilter(typeof(PublishUserHeaderFilter<>), context);
                configurator.UseMessageRetry(AddRetryConfiguration);
            });
        });
        return services;
    }

    private static void AddRetryConfiguration(IRetryConfigurator retryConfigurator)
    {
        retryConfigurator.Exponential(
            3,
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMinutes(120),
            TimeSpan.FromMilliseconds(200));
    }
}
