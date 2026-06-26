using Catalog.Infrastructure;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<SalaryDbContext>();
     

        return services;    
    }

  
}