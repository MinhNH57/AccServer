using SmartAccCloud.Application.Models.Users;

namespace SmartAccCloud.Infrastructure.Persistence.Multitenant;

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