using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.MultiTenancy;

public class MasterDbContext : DbContext
{
    protected MasterDbContext()
    {
    }

    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }

    public DbSet<TenantInfoCustomize> InfoCustomer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TenantInfoCustomize>().ToTable("InformationCustommer");
        modelBuilder.Entity<TenantInfoCustomize>().Property(c => c.Id).HasConversion<int>();
        modelBuilder.Entity<TenantInfoCustomize>().Property(c => c.Identifier).HasColumnName("CompanyId");
    }
}