using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;

namespace Catalog.Base.Infrastructure;

public partial class CatalogDbContext : MultiTenantBaseContext
{
    public CatalogDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
    }

    public CatalogDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<CatalogDbContext> options, ICurrentUser currentUser)
        : base(multiTenantContextAccessor, options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //{
    //    foreach (var item in base.ChangeTracker.Entries()
    //                 .Where(c => c.State == EntityState.Added))
    //    {
    //        if (item.State == EntityState.Added)
    //        {
    //            var type = item.Entity.GetType();
    //            var lstPorp = type.GetProperties();
    //            if (lstPorp.Any(c => c.Name == "CreatedBy"))
    //            {
    //                //TODO: Add CreateBy logic
    //            }
    //            //item.Entity.Created = DateTime.Now;
    //        }
    //    }

    //    return base.SaveChangesAsync(cancellationToken);
    //}
}