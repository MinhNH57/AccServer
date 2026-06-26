using Catalog.HRM.Entities.View;
using Finbuckle.MultiTenant.Abstractions;

namespace Catalog.HRM.Infrastructure;

public partial class HrmDbContext : MultiTenantBaseContext
{
    public HrmDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public HrmDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<HrmDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }
}