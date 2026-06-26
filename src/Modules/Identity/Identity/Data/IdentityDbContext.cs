using Finbuckle.MultiTenant.Abstractions;

namespace Identity.Data;

public class IdentityDbContext : MultiTenantBaseContext
{
    public IdentityDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public IdentityDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<IdentityDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }


    public DbSet<Entites.Users> Users => Set<Entites.Users>();
    public DbSet<RuleUser> RuleUser => Set<RuleUser>();
    public DbSet<CatalogObject> CatalogObject => Set<CatalogObject>();
    public DbSet<UsersRefreshToken> UsersRefreshToken => Set<UsersRefreshToken>();
}