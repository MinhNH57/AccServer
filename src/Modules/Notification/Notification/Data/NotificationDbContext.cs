using BuildingBlocks.MultiTenancy;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.EntityFrameworkCore;
using Notification.Data.Entites;

namespace Notification.Data;

public class NotificationDbContext:MultiTenantBaseContext
{
    public NotificationDbContext(IMultiTenantContextAccessor multiTenantContextAccessor) : base(multiTenantContextAccessor)
    {
    }

    public NotificationDbContext(IMultiTenantContextAccessor multiTenantContextAccessor, DbContextOptions<NotificationDbContext> options) : base(multiTenantContextAccessor, options)
    {
    }

    public DbSet<Users> Users => Set<Users>();
    public DbSet<CatalogRecipientOfMessage> CatalogRecipientOfMessage => Set<CatalogRecipientOfMessage>();
}