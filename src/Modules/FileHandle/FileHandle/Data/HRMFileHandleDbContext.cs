using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using FileHandle.Data.Entites;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileHandle.Data;

public class HRMFileHandleDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public HRMFileHandleDbContext(
        IMultiTenantContextAccessor multiTenantContextAccessor,
        DbContextOptions<HRMFileHandleDbContext> options,
        ICurrentUser currentUser
    ) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var item in ChangeTracker.Entries()
                     .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
        {
            var type = item.Entity.GetType();
            var props = type.GetProperties();

            if (item.State == EntityState.Added)
            {
                var createdBy = props.FirstOrDefault(p => p.Name == "CreatedBy");
                if (createdBy != null) createdBy.SetValue(item.Entity, _currentUser.CodeUser);
            }

            if (item.State == EntityState.Modified)
            {
                var modifiedBy = props.FirstOrDefault(p => p.Name == "ModifiedBy");
                if (modifiedBy != null) modifiedBy.SetValue(item.Entity, _currentUser.CodeUser);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<HRM_SmartFileAttach> SmartFileAttach => Set<HRM_SmartFileAttach>();
    public DbSet<ExcelCatalogObject> ExcelCatalogObject => Set<ExcelCatalogObject>();
}
