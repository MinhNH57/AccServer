using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Infrastructure.Tenants;

public class MultitenantStoreDbContext(DbContextOptions<MultitenantStoreDbContext> options) : EFCoreStoreDbContext<TenantInfoCustomize>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TenantInfoCustomize>().ToTable("InformationCustommer");
        modelBuilder.Entity<TenantInfoCustomize>().Property(c => c.Id).HasConversion<int>();
        modelBuilder.Entity<TenantInfoCustomize>().Property(c => c.Identifier).HasColumnName("CompanyId");

        base.OnModelCreating(modelBuilder);
    }
}