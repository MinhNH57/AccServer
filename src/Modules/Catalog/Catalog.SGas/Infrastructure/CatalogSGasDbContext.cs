using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Catalog.SGas.Entities;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.SGas.Infrastructure;

public partial class CatalogSGasDbContext : MultiTenantBaseContext
{
    public CatalogSGasDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
    }

    public CatalogSGasDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<CatalogSGasDbContext> options, ICurrentUser currentUser)
        : base(multiTenantContextAccessor, options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CategoryWarehose>()
            .ToTable("CategoryWarehose", t => t.UseSqlOutputClause(false));
        modelBuilder.Entity<CatalogObject>()
            .ToTable("CatalogObject", t => t.UseSqlOutputClause(false));
        modelBuilder.Entity<CatalogProduct>()
            .ToTable("CatalogProduct", t => t.UseSqlOutputClause(false));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogSGasDbContext).Assembly);
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