using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using Financial.Infrastructure.Extensions;
using Financial.Model;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure;

/// <remarks>
///     Add migrations using the following command inside the 'Financial.API' project directory:
///     dotnet ef migrations add --context FinancialDbContext [migration-name]
/// </remarks>
public class FinancialDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public FinancialDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;

    }

    public FinancialDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<FinancialDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public DbSet<ReportBalanceSheet> ReportBalanceSheets => Set<ReportBalanceSheet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureFinancial("dbo");
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var item in base.ChangeTracker.Entries()
                     .Where(c => c.State == EntityState.Added))
        {
            if (item.State == EntityState.Added)
            {
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (propertyInfo.Name == "CreatedBy"|| propertyInfo.Name == "CreateBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        break;
                    }
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
