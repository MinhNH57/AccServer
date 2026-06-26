using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using RefNo.API.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RefNo.API.Infrastructure;

public class RefNoDbContext : MultiTenantBaseContext
{
    private readonly ICurrentUser _currentUser;

    public RefNoDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, ICurrentUser currentUser)
        : base(multiTenantContextAccessor)
    {
        _currentUser = currentUser;
    }

    public RefNoDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<RefNoDbContext> options, ICurrentUser currentUser)
        : base(multiTenantContextAccessor, options)
    {
        _currentUser = currentUser;
    }

    public DbSet<NumberingRule> NumberingRules => Set<NumberingRule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RefNoDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var item in base.ChangeTracker.Entries<IBaseEntity>()
                     .Where(c => c.State is EntityState.Added or EntityState.Modified))
        {
            if (item.State == EntityState.Added)
            {
                int updateCount = 0;
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (updateCount == 3) break;
                    if (propertyInfo.Name == "CreatedBy" || propertyInfo.Name == "CreateBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        updateCount++;
                    }
                    else if (propertyInfo.Name is "CreatedDate" or "CreateDate")
                    {
                        propertyInfo.SetValue(item.Entity, DateTime.Now);
                        updateCount++;
                    }
                }
            }
            else if (item.State == EntityState.Modified)
            {
                int updateCount = 0;
                var type = item.Entity.GetType();
                var lstPorp = type.GetProperties();
                foreach (var propertyInfo in lstPorp)
                {
                    if (updateCount == 3) break;
                    if (propertyInfo.Name == "ModifiedBy" || propertyInfo.Name == "ModifyBy")
                    {
                        propertyInfo.SetValue(item.Entity, _currentUser.CodeUser);
                        updateCount++;
                    }
                    else if (propertyInfo.Name == "ModifiedDate")
                    {
                        propertyInfo.SetValue(item.Entity, DateTime.Now);
                        updateCount++;
                    }
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
