using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Voucher.Sgas.Entities;
using Voucher.Sgas.Model.Contracts;

namespace Voucher.Sgas.Infrastructure;

/// <remarks>
///     Add migrations using the following command inside the 'Voucher.API' project directory:
///     dotnet ef migrations add --context VoucherDbContext [migration-name]
/// </remarks>
public class VoucherSgasDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public VoucherSgasDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;

    }

    public VoucherSgasDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<VoucherSgasDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public DbSet<SalesSmartData> SalesSmartData => Set<SalesSmartData>();
    public DbSet<SalesSmartContentsData> SalesSmartContentsData => Set<SalesSmartContentsData>();
    public DbSet<SmartDataPumpCode> SmartDataPumpCode => Set<SmartDataPumpCode>();
    public DbSet<SalesSmartProductInventory> SalesSmartProductInventory => Set<SalesSmartProductInventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoucherSgasDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var item in base.ChangeTracker.Entries<IBaseEntity>()
                     .Where(c => c.State is EntityState.Added or EntityState.Modified))
        {
            if (item.State == EntityState.Added)
            { 
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                item.Entity.CreateBy = _currentUser.CodeUser;
                item.Entity.CreateDate = DateTime.Now; 
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.ModifyBy = _currentUser.CodeUser;
                item.Entity.ModifyDate = DateTime.Now; 
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
