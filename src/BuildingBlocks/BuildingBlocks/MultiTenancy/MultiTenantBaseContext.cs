using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.MultiTenancy;

public class MultiTenantBaseContext : MultiTenantDbContext
{
    private new TenantInfoCustomize? TenantInfo { get; set; }
    private IMultiTenantContextAccessor MultiTenantContextAccessor;
    public MultiTenantBaseContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
        MultiTenantContextAccessor = multiTenantContextAccessor;
    }

    public MultiTenantBaseContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions options) : base(multiTenantContextAccessor, options)
    {
        MultiTenantContextAccessor = multiTenantContextAccessor;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            //foreach (var item in base.ChangeTracker.Entries<ICreatedBase>().Where(c => c.State == EntityState.Added))
            //foreach (var item in base.ChangeTracker.Entries()
            //             .Where(c => c.State == EntityState.Added))
            //{
            //    if (item.State == EntityState.Added)
            //    {
            //        var type = item.Entity.GetType();
            //        var lstPorp = type.GetProperties();
            //        if (lstPorp.Any(c => c.Name == "CreatedBy"))
            //        {
            //            //TODO: Add CreateBy logic
            //        }
            //        //item.Entity.Created = DateTime.Now;
            //    }
            //}
            return base.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        TenantInfo = MultiTenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
        //var tenantInfo = ;
        if (TenantInfo is null)
        {
            //ArgumentNullException.ThrowIfNull(TenantInfo);
            return;
        }

        optionsBuilder.UseSqlServer(TenantInfo.ConnectionString())
            .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information);

    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<CatalogAccountSymbol>()
    //        .ToTable(tb => tb.HasTrigger("UpdateAccountNumber"));
    //    modelBuilder.Entity<CatalogObject>()
    //        .ToTable(tb => tb.HasTrigger("UpdateObject"));
    //    modelBuilder.Entity<CatalogProduct>()
    //        .ToTable(tb => tb.HasTrigger("UpdateProduct"));
    //    modelBuilder.Entity<CategoryWarehose>()
    //        .ToTable(tb => tb.HasTrigger("TrigUpdateWarehoseCode"));
    //}

    public async Task AddEntity(object entity, CancellationToken token)
    {
        await AddAsync(entity, token);
    }

    public async Task AddRangeEntities(IEnumerable<object> entities, CancellationToken token)
    {
        await AddRangeAsync(entities, token);
    }

    public void DeleteRangeEntities(IEnumerable<object> entities, CancellationToken token)
    {
        RemoveRange(entities, token);
    }

    public void UpdateRangeEntities(IEnumerable<object> entities, CancellationToken token)
    {
        foreach (var item in entities)
        {
            Update(item);
        }
    }


    public void UpdateEntity(object entity)
    {
        Update(entity);
    }


}