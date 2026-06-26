using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace BuildingBlocks.OpenApi;

public static class Extensions
{
    // ref: https://github.com/dotnet/eShop/blob/main/src/eShop.ServiceDefaults/OpenApi.Extensions.cs
    public static IServiceCollection AddAspnetOpenApi(this IServiceCollection services)
    {
        string[] versions = ["v1"];

        foreach (var description in versions)
        {
            services.AddOpenApi(
                description,
                options =>
                {
                    options.AddDocumentTransformer<SecuritySchemeDocumentTransformer>();
                });
        }
        return services;
    }

    public static IApplicationBuilder UseAspnetOpenApi(this WebApplication app)
    {
        app.MapOpenApi();

        app.UseSwaggerUI(
            options =>
            {
                var descriptions = app.DescribeApiVersions();

                // build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var openApiUrl = $"/openapi/{description.GroupName}.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(openApiUrl, name);
                }
            });

        // Add scalar ui
        app.MapScalarApiReference(
            options =>
            {
                options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
            });
        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
        return app;
    }
}