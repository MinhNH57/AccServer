using BuildingBlocks.MultiTenancy;
using BuildingBlocks.Web;
using FileHandle.Data.Entites;
using FileHandle.Models;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FileHandle.Data;

public class FileHandleDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;
    public FileHandleDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser) : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;
    }

    public FileHandleDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<FileHandleDbContext> options, ICurrentUser currentUser) : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var item in base.ChangeTracker.Entries()
                     .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
        {
            if (item.State == EntityState.Added)
            {
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (propertyInfo.Name == "CreatedBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        break;
                    }
                }
            }
            if (item.State == EntityState.Modified)
            {
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    
                    if (propertyInfo.Name == "ModifiedBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        break;
                    }
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<SmartFileAttach> SmartFileAttach => Set<SmartFileAttach>();
    public DbSet<ExcelCatalogObject> ExcelCatalogObject => Set<ExcelCatalogObject>(); 
    public DbSet<OptionPrintOrder> OptionPrintOrder => Set<OptionPrintOrder>();
    public DbSet<ExcelSmartData> ExcelSmartData => Set<ExcelSmartData>();

}