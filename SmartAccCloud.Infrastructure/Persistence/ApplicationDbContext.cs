using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartAccCloud.Application.Data;
using SmartAccCloud.Application.Models.Users;
using SmartAccCloud.Domain.Entity.Commons;

namespace SmartAccCloud.Infrastructure.Persistence;

public partial class ApplicationDbContext : MultiTenantDbContext, IApplicationDbContext
{
    private new TenantInfoCustomize? TenantInfo { get; set; }
    public ApplicationDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
        TenantInfo = multiTenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    }

    public ApplicationDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions options) : base(multiTenantContextAccessor, options)
    {
        TenantInfo = multiTenantContextAccessor.MultiTenantContext.TenantInfo as TenantInfoCustomize;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            foreach (var item in base.ChangeTracker.Entries<ICreatedBase>()
                         .Where(c => c.State == EntityState.Added))
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.Created = DateTime.Now;
                }
            }
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
        //var tenantInfo = ;
        if (TenantInfo is null)
        {
            IConfiguration config = new ConfigurationManager();
            var conn = config.GetConnectionString("MultitenantConnection")!;
            optionsBuilder.UseSqlServer(conn);
        }
        else
        {
            //throw new ArgumentException("TenantInfo is invalid");
            optionsBuilder.UseSqlServer(TenantInfo.ConnectionString())
                .LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogAccountSymbol>()
            .ToTable(tb => tb.HasTrigger("UpdateAccountNumber"));
        modelBuilder.Entity<CatalogObject>()
            .ToTable(tb => tb.HasTrigger("UpdateObject"));
        modelBuilder.Entity<CatalogProduct>()
            .ToTable(tb => tb.HasTrigger("UpdateProduct"));
        modelBuilder.Entity<CategoryWarehose>()
            .ToTable(tb => tb.HasTrigger("TrigUpdateWarehoseCode"));
    }

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