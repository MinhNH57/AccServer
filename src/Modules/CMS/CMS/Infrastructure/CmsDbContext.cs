using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure;

/// <remarks>
///     Add migrations using the following command inside the 'Cms.API' project directory:
///     dotnet ef migrations add --context CmsDbContext [migration-name]
/// </remarks>
public class CmsDbContext: MultiTenantBaseContext
{
    public CmsDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public CmsDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<CmsDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CmsDbContext).Assembly);
    }
}
